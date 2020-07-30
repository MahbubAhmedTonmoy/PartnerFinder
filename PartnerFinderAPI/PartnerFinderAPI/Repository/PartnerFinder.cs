﻿using Microsoft.EntityFrameworkCore;
using PartnerFinderAPI.DB;
using PartnerFinderAPI.DTO;
using PartnerFinderAPI.Model;
using PartnerFinderAPI.Pagging;
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
            result = result.Where(x => x.Gender == paggingParms.Gender);

            //sort
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

    }
}
