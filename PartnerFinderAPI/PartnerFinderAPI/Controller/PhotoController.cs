using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PartnerFinderAPI.DTO;
using PartnerFinderAPI.Helpers;
using PartnerFinderAPI.Migrations;
using PartnerFinderAPI.Model;
using PartnerFinderAPI.Pagging;
using PartnerFinderAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PartnerFinderAPI.Controller
{
    [Route("api/user/{userId}/[controller]")]
    [ApiController]
    [Authorize]
    public class PhotoController : ControllerBase
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _options;
        private Cloudinary _cloudinary;
        public PhotoController(IUnitofWork unitofWork, IMapper mapper, IOptions<CloudinarySettings> options)
        {
            _unitofWork = unitofWork;
            _mapper = mapper;
            _options = options;
            Account account = new Account(_options.Value.CloudName, _options.Value.ApiKey, _options.Value.ApiSecret);
            _cloudinary = new Cloudinary(account);
        }

        [HttpGet("{id}", Name = "GetPhoto")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            var photoFromRepo = await _unitofWork.PhotoRepo.GetPhoto(id);

            var photo = _mapper.Map<PhotoReturnDto>(photoFromRepo);

            return Ok(photo);
        }

        [HttpPost("up")]
        public async Task<IActionResult> AddPhotoForUser(string userId, PhotoUploadDTO photoUploadDTO)
        {
            if (userId != User.FindFirst(ClaimTypes.NameIdentifier).Value)
                return Unauthorized();
            var userexist = await _unitofWork.PartnerFinder.GetUser(userId);
            if (userexist == null) return BadRequest("user not found");

            var photoFile = photoUploadDTO.File;
            var uploadResult = new ImageUploadResult();

            if (photoFile.Length > 0)
            {
                using (var stream = photoFile.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(photoFile.Name, stream),
                        Transformation = new Transformation()
                            .Width(500).Height(500).Crop("fill").Gravity("face")
                    };
                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }
            photoUploadDTO.Url = uploadResult.Uri.ToString();
            photoUploadDTO.PublicId = uploadResult.PublicId;
            var photo = _mapper.Map<Photo>(photoUploadDTO);

            if (!userexist.Photos.Any(x => x.IsMain))
            {
                photo.IsMain = true;
            }
            userexist.Photos.Add(photo);

            var result = await _unitofWork.Save();
            if (result == 0) return StatusCode(500, "saving probem");
            var phototoReturn = _mapper.Map<PhotoReturnDto>(photo);
            return Ok(phototoReturn);
        }


        [HttpPost("{id}/setMain")]
        public async Task<IActionResult> SetMainPhoto(string userId, int id)
        {
            if (userId != User.FindFirst(ClaimTypes.NameIdentifier).Value)
                return Unauthorized();

            var user = await _unitofWork.PartnerFinder.GetUser(userId);

            if (!user.Photos.Any(p => p.Id == id))
                return BadRequest("this photo is not present in this user profile");

            var photoFromRepo = await _unitofWork.PhotoRepo.GetPhoto(id);

            if (photoFromRepo.IsMain)
                return BadRequest("This is already the main photo");

            var currentMainPhoto = await _unitofWork.PhotoRepo.GetMainPhotoForUser(userId);
            currentMainPhoto.IsMain = false;

            photoFromRepo.IsMain = true;

            var result = await _unitofWork.Save();
            if (result == 0) return StatusCode(500, "saving probem");

            return Ok("set photo to main");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto(string userId, int photoId)
        {
            if (userId != User.FindFirst(ClaimTypes.NameIdentifier).Value)
                return Unauthorized();

            var user = await _unitofWork.PartnerFinder.GetUser(userId);

            if (!user.Photos.Any(p => p.Id == id))
                return BadRequest("this photo is not present in this user profile");

            var photoFromRepo = await _unitofWork.PhotoRepo.GetPhoto(photoId);

            if (photoFromRepo.IsMain) return BadRequest("not able to delete main photo");
            if(photoFromRepo.PublicId != null)
            {
                var deleteParams = new DeletionParams(photoFromRepo.PublicId);
                var deleteresult = _cloudinary.Destroy(deleteParams);
                if(deleteresult.Result == "ok")
                {
                    _unitofWork.PhotoRepo.Delete(photoFromRepo);
                }
            }
            if(photoFromRepo.PublicId == null)
            {
                _unitofWork.PhotoRepo.Delete(photoFromRepo);
            }
            var result = await _unitofWork.Save();
            if (result == 0) return StatusCode(500, "saving probem");
            return Ok("delete photo");
        }

    }
}
