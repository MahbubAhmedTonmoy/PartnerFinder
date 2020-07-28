using AutoMapper;
using PartnerFinderAPI.DTO;
using PartnerFinderAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartnerFinderAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, UserForListDto>();
            CreateMap<AppUser, UserForDetailedDto>();
        }
    }
}
