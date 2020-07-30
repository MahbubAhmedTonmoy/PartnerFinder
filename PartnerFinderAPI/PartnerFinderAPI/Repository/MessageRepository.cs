using Microsoft.EntityFrameworkCore;
using PartnerFinderAPI.DB;
using PartnerFinderAPI.Model;
using PartnerFinderAPI.Pagging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartnerFinderAPI.Repository
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        private readonly AppDbContext _db;
        public MessageRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<Message> GetMessage(int id)
        {
            return await _db.Messages.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<PagedList<Message>> GetMessagesForUser()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Message>> GetMessagesThread(string senderId, string receiverId)
        {
            throw new NotImplementedException();
        }
        public void Add(Message entity)
        {
            _db.Messages.Add(entity);
        }

    }
}
