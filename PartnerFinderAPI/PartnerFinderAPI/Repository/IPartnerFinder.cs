using PartnerFinderAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartnerFinderAPI.Repository
{
    public interface IPartnerFinder: IRepository<AppUser>
    {
        Task<IEnumerable<AppUser>> GetUsers();
        Task<AppUser> GetUser(string id);
    }
}
