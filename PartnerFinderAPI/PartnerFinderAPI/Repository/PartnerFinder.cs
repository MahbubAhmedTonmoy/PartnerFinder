using Microsoft.EntityFrameworkCore;
using PartnerFinderAPI.DB;
using PartnerFinderAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartnerFinderAPI.Repository
{
    public class PartnerFinder : Repository<AppUser>, IPartnerFinder
    {
        private readonly AppDbContext _db;
        public PartnerFinder(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<AppUser> GetUser(string email)
        {
            var ans =await _db.AppUsers.Include(x => x.Photos).FirstOrDefaultAsync(x=> x.Email == email);
            return ans;
        }

        public async Task<IEnumerable<AppUser>> GetUsers()
        {
            var result = await _db.AppUsers.Include(x => x.Photos).ToListAsync();
            return result;
        }
    }
}
