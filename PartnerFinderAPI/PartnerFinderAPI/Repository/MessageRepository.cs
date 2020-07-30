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

        public async Task<PagedList<Message>> GetMessagesForUser(MessageParms messageParms)
        {
            var messages = _db.Messages.Include(x => x.Sender).ThenInclude(p => p.Photos)
                .Include(y => y.Receiver).ThenInclude(p => p.Photos).AsQueryable();

            switch (messageParms.MessageStatus)
            {
                case "Inbox":
                    messages = messages.Where(x => x.ReceiverId == messageParms.UserId);
                    break;
                case "Outbox":
                    messages = messages.Where(x => x.SenderId == messageParms.UserId);
                    break;
                default:
                    messages = messages.Where(x => x.ReceiverId == messageParms.UserId && x.IsRead == false);
                    break;
            }
            messages = messages.OrderByDescending(x => x.MessageSentTime);

            return await PagedList<Message>.CreteAsync(messages, messageParms.PageNumber, messageParms.PageSize);
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
