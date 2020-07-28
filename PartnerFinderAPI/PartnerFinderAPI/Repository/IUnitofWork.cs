using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartnerFinderAPI.Repository
{
    public interface IUnitofWork
    {
        IPartnerFinder PartnerFinder { get; }

        Task<int> Save();
    }
}
