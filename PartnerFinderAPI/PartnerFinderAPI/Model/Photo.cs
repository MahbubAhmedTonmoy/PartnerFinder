using System;

namespace PartnerFinderAPI.Model
{
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }
        //casecade delete 
        public AppUser User { get; set; }
        public string UserId { get; set; }
    }
}