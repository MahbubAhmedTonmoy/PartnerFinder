using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PartnerFinderAPI.DB;
using PartnerFinderAPI.Model;
using PartnerFinderAPI.Pagging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
                    messages = messages.Where(x => x.ReceiverId == messageParms.UserId && x.ReceiverDeleted == false);
                    break;
                case "Outbox":
                    messages = messages.Where(x => x.SenderId == messageParms.UserId && x.SenderDeleted == false);
                    break;
                default:
                    messages = messages.Where(x => x.ReceiverId == messageParms.UserId && x.ReceiverDeleted == false && x.IsRead == false);
                    break;
            }
            messages = messages.OrderByDescending(x => x.MessageSentTime);

            return await PagedList<Message>.CreteAsync(messages, messageParms.PageNumber, messageParms.PageSize);
        }

        public async Task<IEnumerable<Message>> GetMessagesThread(string senderId, string receiverId)
        {
            var messages = await _db.Messages.Include(x => x.Sender).ThenInclude(x => x.Photos)
                                               .Include(y => y.Receiver).ThenInclude(y => y.Photos)
                                               .Where(x => x.SenderId == senderId && x.ReceiverId == receiverId && x.SenderDeleted == false
                                               || x.SenderId == receiverId && x.ReceiverId == senderId && x.ReceiverDeleted == false)
                                               .OrderBy(m => m.MessageSentTime).ToListAsync();
            return messages;
        }
        public void Add(Message entity)
        {
            _db.Messages.Add(entity);
        }


    }
}
