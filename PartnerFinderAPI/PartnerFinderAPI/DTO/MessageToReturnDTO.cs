using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartnerFinderAPI.DTO
{
    public class MessageToReturnDTO
    {
        public int Id { get; set; }
        public string SenderId { get; set; }
        public string SenderPhotoUrl { get; set; }
        public string ReceiverId { get; set; }
        public string ReceiverPhotoUrl { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateReadTime { get; set; }
        public DateTime MessageSentTime { get; set; }
    }
}
