using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartnerFinderAPI.Model
{
    public class Message
    {
        public int Id { get; set; }
        public string SenderId { get; set; }
        public AppUser Sender { get; set; }
        public string ReceiverId { get; set; }
        public AppUser Receiver { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateReadTime { get; set; }
        public DateTime MessageSentTime { get; set; }
        public bool SenderDeleted { get; set; }
        public bool ReceiverDeleted { get; set; }
    }
}
