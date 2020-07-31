using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PartnerFinderAPI.DTO;
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
    [Route("api/user/{senderId}/[controller]")]
    [ApiController]
    [Authorize]
    public class MessageController : ControllerBase
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IMapper _mapper;
        public MessageController(IUnitofWork unitofWork, IMapper mapper)
        {
            _unitofWork = unitofWork;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name ="GetMessage")]
        public async Task<IActionResult> GetMessage(int id)
        {
            var messageFromRepo = await _unitofWork.MessageRepository.GetMessage(id);
            if (messageFromRepo == null)
                return NotFound();
            return Ok(messageFromRepo);
        }

        [HttpPost]
        public async Task<IActionResult> Createmessage(string senderId, MessageCreateDTO messageCreateDTO)
        {
            var a = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (senderId != User.FindFirst(ClaimTypes.NameIdentifier).Value)
                return Unauthorized();
            var receiver = await _unitofWork.PartnerFinder.GetUser(messageCreateDTO.ReceiverId);
            if (receiver == null)
                return BadRequest("receiver not found");
            var message = _mapper.Map<Message>(messageCreateDTO);
            _unitofWork.MessageRepository.Add(message);
            var result =await _unitofWork.Save();
            if (result == 0) return StatusCode(500, "saving probem");
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult>GetMessagesForUser(string senderId, [FromQuery] MessageParms messageParms)
        {
            var a = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (senderId != User.FindFirst(ClaimTypes.NameIdentifier).Value)
                return Unauthorized();

            messageParms.UserId = senderId;
            var messageFromRepo = await _unitofWork.MessageRepository.GetMessagesForUser(messageParms);
            var message = _mapper.Map<IEnumerable<MessageToReturnDTO>>(messageFromRepo);
            return Ok(message);

        }
        [HttpGet("thread/{receiverID}")]
        public async Task<IActionResult> GetMessageThread(string senderId, string receiverId)
        {
            var a = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (senderId != User.FindFirst(ClaimTypes.NameIdentifier).Value)
                return Unauthorized();
            var mesFromRepo = await _unitofWork.MessageRepository.GetMessagesThread(senderId, receiverId);
            var messageThread = _mapper.Map<IEnumerable<MessageToReturnDTO>>(mesFromRepo);
            return Ok(messageThread);
        }

        [HttpPost("{id}/delete")]
        public async Task<IActionResult> DeleteMessage(int id, string senderId)
        {
            var a = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (senderId != User.FindFirst(ClaimTypes.NameIdentifier).Value)
                return Unauthorized();

            var messageExist = await _unitofWork.MessageRepository.GetMessage(id);
            if (messageExist != null)
            {
                if (messageExist.SenderId == senderId)
                    messageExist.SenderDeleted = true;
                if (messageExist.ReceiverId == senderId)
                    messageExist.ReceiverDeleted = true;
                if (messageExist.SenderDeleted && messageExist.ReceiverDeleted)
                    _unitofWork.MessageRepository.Delete(messageExist);
                var result = await _unitofWork.Save();
                if (result == 0) return StatusCode(500, "saving probem");
                return Ok();
            }
            return StatusCode(404, "message not found");
        }
    }
}
