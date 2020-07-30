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
            CreateMap<AppUser, UserForListDto>()
                .ForMember(destination => destination.PhotoUrl, opt =>
                {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain == true).Url);
                });

            CreateMap<AppUser, UserForDetailedDto>()
                .ForMember(destination => destination.PhotoUrl, opt =>
                {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain == true).Url);
                });
            CreateMap<Photo, PhotosForDetailedDto>();

            CreateMap<UserForUpdateDto, AppUser>();
            CreateMap<MessageCreateDTO, Message>().ReverseMap();
            CreateMap<Message, MessageToReturnDTO>()
                .ForMember(destination => destination.SenderPhotoUrl, opt =>
                {
                    opt.MapFrom(u => u.Sender.Photos.FirstOrDefault(x => x.IsMain == true).Url);
                })
                .ForMember(destination => destination.ReceiverPhotoUrl, opt =>
                 {
                     opt.MapFrom(u => u.Receiver.Photos.FirstOrDefault(x => x.IsMain == true).Url);
                 });
        }
    }
}
