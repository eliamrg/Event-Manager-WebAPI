using AutoMapper;
using Event_manager_API.DTOs.Get;
using Event_manager_API.DTOs.Set;
using Event_manager_API.Entities;

namespace Event_manager_API.Utilities
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles() 
        {
            
            //DTO SET 
            CreateMap<CouponDTO, Coupon>();
            CreateMap<EventDTO, Event>();
            CreateMap<FavouriteDTO, Favourite>();
            CreateMap<FollowDTO, Follow>();
            CreateMap<FormDTO, Form>();
            CreateMap<LocationDTO, Location>();
            CreateMap<TicketDTO, Ticket>();
            CreateMap<UserDTO, User>();


            //DTO GET
            CreateMap<Coupon, GetCouponDTO>();
            CreateMap<Event, GetEventDTO>();
            CreateMap<Favourite, GetFavouriteDTO>();
            CreateMap<Follow, GetFollowDTO>();
            CreateMap<Form, GetFormDTO>();
            CreateMap<Location, GetLocationDTO>();
            CreateMap<Ticket, GetTicketDTO>();
            CreateMap<User, GetUserDTO>();
        }
    }
}
