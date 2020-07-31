using PartnerFinderAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartnerFinderAPI.Repository
{
    public interface IPhotoRepo : IRepository<Photo>
    {
        Task<Photo> GetPhoto(int id);
        Task<Photo> GetMainPhotoForUser(string userId);
    }
}
