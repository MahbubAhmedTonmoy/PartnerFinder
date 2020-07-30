using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartnerFinderAPI.Model
{
    public class Like
    {
        public string LikerID { get; set; }
        public string LikeeID { get; set; }
        public AppUser Liker { get; set; }
        public AppUser Likee { get; set; }
    }
}
