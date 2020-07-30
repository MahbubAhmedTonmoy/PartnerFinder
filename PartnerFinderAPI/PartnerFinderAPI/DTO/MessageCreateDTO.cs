using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartnerFinderAPI.DTO
{
    public class MessageCreateDTO
    {
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public DateTime MessageSentTime { get; set; }
        public string Content { get; set; }
        public MessageCreateDTO()
        {
            MessageSentTime = DateTime.Now;
        }
    }
}
