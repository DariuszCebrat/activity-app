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
            
        }
    }
}
