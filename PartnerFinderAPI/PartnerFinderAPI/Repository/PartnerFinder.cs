using Microsoft.EntityFrameworkCore;
using PartnerFinderAPI.DB;
using PartnerFinderAPI.DTO;
using PartnerFinderAPI.Model;
using PartnerFinderAPI.Pagging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

        public Like GetLike(string senderId, string receiverid)
        {
            var likeExist =  _db.Likes.Where(x => x.LikerID == senderId && x.LikeeID == receiverid).FirstOrDefault();
            return likeExist;
        }

        public async Task<AppUser> GetUser(string id)
        {
            try
            {
                var ans = await _db.AppUsers.Include(x => x.Photos).FirstOrDefaultAsync(x => x.Id == id);
                return ans;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<PagedList<AppUser>> GetUsers(PaggingParms paggingParms)
        {
            var result =  _db.AppUsers.Include(x => x.Photos).OrderBy(x=> x.LastActive).AsQueryable();
            //filter
            if (!string.IsNullOrEmpty(paggingParms.Gender))
                result = result.Where(x => x.Gender == paggingParms.Gender);
            if (paggingParms.Likers)
            {
                var userLikers = await GetUserLikes(paggingParms.UserId, paggingParms.Likers);
                result = result.Where(u => userLikers.Contains(u.Id));
            }
            if (paggingParms.Likees)
            {
                var userLikees = await GetUserLikes(paggingParms.UserId, paggingParms.Likers);
                result = result.Where(u => userLikees.Contains(u.Id));
            }
            // sort
            if (!string.IsNullOrEmpty(paggingParms.OrderBy))
            {
                switch (paggingParms.OrderBy)
                {
                    case "created":
                        result = result.OrderByDescending(x => x.Created);
                        break;
                    default:
                        result = result.OrderByDescending(x => x.LastActive);
                        break;
                }
            }

            return await PagedList<AppUser>.CreteAsync(result, paggingParms.PageNumber, paggingParms.PageSize);
        }

        public void SendLike(string senderId, string receiverID)
        {
            try
            {
                var senderExist = _db.AppUsers.FirstOrDefault(x => x.Id == senderId);
                var receiverExist = _db.AppUsers.FirstOrDefault(x => x.Id == receiverID);

                if (senderId != null || receiverID != null)
                {
                    var like = new Like
                    {
                        LikerID = senderId,
                        LikeeID = receiverID
                    };
                    _db.Likes.Add(like);
                }
               
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private async Task<IEnumerable<string>> GetUserLikes(string id, bool likers)
        {
            var user = await _db.AppUsers
                .Include(x => x.Likers).Include(y => y.Likees)
                .FirstOrDefaultAsync(u => u.Id == id);
            if (likers)
            {
                return user.Likers.Where(u => u.LikeeID == id).Select(i => i.LikerID);
            }
            else
            {
                return user.Likers.Where(u => u.LikerID == id).Select(i => i.LikeeID);
            }
        }
    }
}
