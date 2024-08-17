using AutoMapper;
using secondProject.Dtos.DestinationDtos;
using secondProject.Dtos.ThingsToDoDtos;
using secondProject.Models;


namespace secondProject.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ThingsToDo,ThingsToDoDto>();
            CreateMap<ThingsToDoDto, ThingsToDo>();
            CreateMap<Destination, GetDestinationDto>();
            CreateMap<GetDestinationDto,Destination>();
        }
    }
}
