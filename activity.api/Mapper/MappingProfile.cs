using activity.api.CQRS_Functions.Command.ActivityCommand;
using activity.api.DTO.ActivityDto;
using activity.domain.Entities;
using AutoMapper;

namespace activity.api.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateActivityCommand.Request, Activity>();
            CreateMap<EditActivityCommand.Request, Activity>();
            CreateMap<EditActivityDto,EditActivityCommand.Request>();
            CreateMap<CreateActivityDto,CreateActivityCommand.Request>();
            CreateMap<Activity, ActivityDto>()
                .ForMember(x=>x.HostUserName, d=>d.MapFrom(s=>s.Attendies.FirstOrDefault(x=>x.IsHost).AppUser.UserName))
                .ForMember(x=>x.Profiles,d=>d.MapFrom(s=>s.Attendies));
            CreateMap<ActivityAttendee, activity.api.DTO.IdentityDto.Profile>()
                .ForMember(x => x.UserName, d => d.MapFrom(s => s.AppUser.UserName))
                .ForMember(x => x.DisplayName, d => d.MapFrom(s => s.AppUser.DisplayName))
                .ForMember(x => x.Bio, d => d.MapFrom(s => s.AppUser.Bio));
          
            
        }
    }
}
