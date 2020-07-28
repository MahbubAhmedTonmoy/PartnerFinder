using PartnerFinderAPI.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartnerFinderAPI.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _db;
        public Repository(AppDbContext db)
        {
            _db = db;
        }

        public void Add(T entity)
        {
            _db.Add(entity);
        }


        public void Delete(T entity)
        {
            _db.Remove(entity);
        }
    }
}
