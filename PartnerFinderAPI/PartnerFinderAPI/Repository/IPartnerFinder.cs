using PartnerFinderAPI.DTO;
using PartnerFinderAPI.Model;
using PartnerFinderAPI.Pagging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartnerFinderAPI.Repository
{
    public interface IPartnerFinder: IRepository<AppUser>
    {
        Task<PagedList<AppUser>> GetUsers(PaggingParms paggingParms);
        Task<AppUser> GetUser(string id);
        Like GetLike(string senderId, string receiverid);
        void SendLike(string senderId, string receiverID);

        Task<Message> GetMessage(int id);
        Task<PagedList<Message>> GetMessagesForUser();
        Task<IEnumerable<Message>> GetMessagesThread(string senderId, string receiverId);
    }
}
