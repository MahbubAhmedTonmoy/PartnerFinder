using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PartnerFinderAPI.DTO;
using PartnerFinderAPI.Model;
using PartnerFinderAPI.Pagging;
using PartnerFinderAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace PartnerFinderAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IMapper _mapper;
        public UserController(IUnitofWork unitofWork, IMapper mapper)
        {
            _unitofWork = unitofWork;
            _mapper = mapper;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll([FromQuery]PaggingParms paggingParms)
        {
            try
            {
                var users = await _unitofWork.PartnerFinder.GetUsers(paggingParms);
                var userToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);
                return Ok(userToReturn);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetByEmail(string id)
        {
            try
            {
                var user = await _unitofWork.PartnerFinder.GetUser(id);
                if (user == null)
                {
                    return StatusCode(404, "not found");
                }
                var userToReturn = _mapper.Map<UserForDetailedDto>(user);
                return Ok(userToReturn);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Update(string id, UserForUpdateDto userForUpdate)
        {
            try
            {
                var findUser = _unitofWork.PartnerFinder.GetUser(id);
                var user = findUser.Result;
                if(findUser.Result != null)
                {
                    _mapper.Map(userForUpdate, user);
                    var result =await _unitofWork.Save();
                    if (result == 0) return StatusCode(500, "not saved");
                    return Ok();
                }
                return StatusCode(400, "not found");
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        [HttpPost("{sender}/like/{receiver}")]
        public async Task<IActionResult> Like(string sender, string receiver)
        {
            try
            {
                var likeExist = _unitofWork.PartnerFinder.GetLike(sender, receiver);
                if (likeExist != null)
                {
                    return BadRequest("already likes this");
                }
                else
                {
                    _unitofWork.PartnerFinder.SendLike(sender, receiver);
                    var result = await _unitofWork.Save();
                    if (result == 0) return StatusCode(500, "not saved");
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
