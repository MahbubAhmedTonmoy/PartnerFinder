﻿using PartnerFinderAPI.Model;
using PartnerFinderAPI.Pagging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartnerFinderAPI.Repository
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<Message> GetMessage(int id);
        Task<PagedList<Message>> GetMessagesForUser(MessageParms messageParms); // read, unread, inbox outbox
        Task<IEnumerable<Message>> GetMessagesThread(string senderId, string receiverId);

        void Add(Message entity);
    }
}
