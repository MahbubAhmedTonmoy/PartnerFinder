using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartnerFinderAPI.Pagging
{
    public class MessageParms
    {
        private const int MaxPageSize = 100;
        public int PageNumber { get; set; } = 1;

        private int pageSize = 10;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }
        public string UserId { get; set; }
        public string MessageStatus { get; set; } = "Unread";
    }
}
