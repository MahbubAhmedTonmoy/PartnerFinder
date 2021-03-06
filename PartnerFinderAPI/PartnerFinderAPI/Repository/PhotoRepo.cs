﻿using Microsoft.EntityFrameworkCore;
using PartnerFinderAPI.DB;
using PartnerFinderAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace PartnerFinderAPI.Repository
{
    public class PhotoRepo : Repository<Photo>, IPhotoRepo
    {
        private readonly AppDbContext _db;
        public PhotoRepo(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Photo> GetMainPhotoForUser(string userId)
        {
            var userMainPhoto = await _db.Photos.Where(x => x.UserId == userId).FirstOrDefaultAsync(x => x.IsMain);
        }

        public async Task<Photo> GetPhoto(int id)
        {
            return await _db.Photos.FirstOrDefaultAsync(x => x.Id == id);
        }


    }
}
