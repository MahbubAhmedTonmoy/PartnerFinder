using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartnerFinderAPI.DTO
{
    public class PhotoUploadDTO
    {
        public string Url { get; set; }
        public IFormFile File { get; set; } // photo r file abc.pnj
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public string PublicId { get; set; }

        public PhotoUploadDTO()
        {
            DateAdded = DateTime.Now;
        }
    }

    public class PhotoReturnDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
    }
}
