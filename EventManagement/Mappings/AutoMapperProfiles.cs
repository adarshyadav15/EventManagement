using AutoMapper;
using EventManagement.Models.Domain;
using EventManagement.Models.DTO;

namespace EventManagement.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<User, CreateUserDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UpdateUserDTO>().ReverseMap();
            CreateMap<Event, EventDTO>().ReverseMap();
            CreateMap<Event, CreateEventDTO>().ReverseMap();
            CreateMap<Event, UpdateEventDTO>().ReverseMap();
            CreateMap<EventUser, AddEventUserDTO>().ReverseMap();
            CreateMap<EventUser, EventUserDTO>().ReverseMap();
            CreateMap<Event, DisplayEventDTO>().ReverseMap();
            CreateMap<User, DisplayUserDTO>().ReverseMap();
            CreateMap<Feedback, FeedbackDTO>().ReverseMap();
            CreateMap<Feedback, CreateFeedbackDTO>().ReverseMap();
            CreateMap<Feedback, UpdateFeedbackDTO>().ReverseMap();
        }
    }
}
