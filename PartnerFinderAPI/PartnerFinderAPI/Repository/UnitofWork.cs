﻿using PartnerFinderAPI.DB;
using PartnerFinderAPI.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartnerFinderAPI.Repository
{
    public class UnitofWork : IUnitofWork
    {
        public IPartnerFinder PartnerFinder { get; private set; }
        public IMessageRepository MessageRepository { get; private set; }
        public IPhotoRepo PhotoRepo { get; private set; }
        private readonly AppDbContext _db;
        public UnitofWork(AppDbContext db)
        {
            _db = db;
            PartnerFinder = new PartnerFinder(_db);
            MessageRepository = new MessageRepository(_db);
            PhotoRepo = new PhotoRepo(_db);
        }

        public async Task<int> Save()
        {
            var result = await _db.SaveChangesAsync();
            return result;
        }
    }
}
