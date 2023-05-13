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
            CreateMap<Coupon, GetCouponDTOwithTickets>()
                .ForMember(DTO => DTO.Tickets, opt => opt.MapFrom(MapGetCouponDTOwithTickets));
            //----------------------------------------------------------------------------------
            CreateMap<Event, GetEventDTO>();
            CreateMap<Event, GetEventDTOwithCoupons>()
                .ForMember(DTO => DTO.Coupons, opt => opt.MapFrom(MapGetEventDTOwithCoupons));
            CreateMap<Event, GetEventDTOwithForms>()
                .ForMember(DTO => DTO.FormResponses, opt => opt.MapFrom(MapGetEventDTOwithForms));
            CreateMap<Event, GetEventDTOwithTickets>()
                .ForMember(DTO => DTO.Tickets, opt => opt.MapFrom(MapGetEventDTOwithTickets));

            //----------------------------------------------------------------------------------
            CreateMap<Favourite, GetFavouriteDTO>();

            //----------------------------------------------------------------------------------
            CreateMap<Follow, GetFollowDTO>();

            //----------------------------------------------------------------------------------
            CreateMap<Form, GetFormDTO>();

            //----------------------------------------------------------------------------------
            CreateMap<Location, GetLocationDTO>();
            CreateMap<Location, GetLocationDTOwithEvents>()
                .ForMember(DTO => DTO.EventsList, opt => opt.MapFrom(MapGetLocationDTOwithEvents));
            //----------------------------------------------------------------------------------
            CreateMap<Ticket, GetTicketDTO>();

            //----------------------------------------------------------------------------------
            CreateMap<User, GetUserDTO>();
            CreateMap<User, GetUserDTOwithFavourites>()
                .ForMember(DTO => DTO.Favourites, opt => opt.MapFrom(MapGetUserDTOwithFavourites));
            CreateMap<User, GetUserDTOwithFollowers>()
                .ForMember(DTO => DTO.Followers, opt => opt.MapFrom(MapGetUserDTOwithFollowers));
            CreateMap<User, GetUserDTOwithFollowing>()
                .ForMember(DTO => DTO.Following, opt => opt.MapFrom(MapGetUserDTOwithFollowing));
            CreateMap<User, GetUserDTOwithForms>()
                .ForMember(DTO => DTO.FormResponses, opt => opt.MapFrom(MapGetUserDTOwithForms));
            CreateMap<User, GetUserDTOwithTickets>()
                .ForMember(DTO => DTO.Tickets, opt => opt.MapFrom(MapGetUserDTOwithTickets));


        }
        //MAPPERS--------------------------------------------------------------------------------------------------


        private List<GetSimpleTicketDTO> MapGetCouponDTOwithTickets(Coupon entity, GetCouponDTO getDTO)
        {
            var result = new List<GetSimpleTicketDTO>();
            if (entity.Tickets == null)
            {
                return result;
            }
            foreach (var record in entity.Tickets)
            {
                result.Add(new GetSimpleTicketDTO()
                {
                    Id = record.Id,
                    TicketPrice = record.TicketPrice,
                    UserId  = record.UserId,
                    EventId = record.EventId,
                    CouponId = record.CouponId,
                   
                });
            }
            return result;
        }
//---------------------------------------------------------------------------------------------------------------------
        private List<GetSimpleCouponDTO> MapGetEventDTOwithCoupons(Event entity, GetEventDTO getDTO)
        {
            var result = new List<GetSimpleCouponDTO>();
            if (entity.Coupons == null)
            {
                return result;
            }
            foreach (var record in entity.Coupons)
            {
                result.Add(new GetSimpleCouponDTO()
                {
                    Id = record.Id,
                    Code = record.Code,
                    DiscountPercentage = record.DiscountPercentage, 
                    EventId= record.EventId,
                });
            }
            return result;
        }

        private List<GetFormDTO> MapGetEventDTOwithForms(Event entity, GetEventDTO getDTO)
        {
            var result = new List<GetFormDTO>();
            if (entity.FormResponses == null)
            {
                return result;
            }
            foreach (var record in entity.FormResponses)
            {
                result.Add(new GetFormDTO()
                {
                    Id = record.Id,
                    Comment = record.Comment,
                    CreatedAt = record.CreatedAt,
                    EventId= record.EventId,
                    UserId= record.UserId,
                    

                });
            }
            return result;
        }


        private List<GetSimpleTicketDTO> MapGetEventDTOwithTickets(Event entity, GetEventDTO getDTO)
        {
            var result = new List<GetSimpleTicketDTO>();
            if (entity.Tickets == null)
            {
                return result;
            }
            foreach (var record in entity.Tickets)
            {
                result.Add(new GetSimpleTicketDTO()
                {
                    Id = record.Id,
                    TicketPrice = record.TicketPrice,
                    UserId = record.UserId,
                    EventId = record.EventId,
                    CouponId = record.CouponId,

                });
            }
            return result;
        }

       
//---------------------------------------------------------------------------------------------------------------------
        private List<GetSimpleEventDTO> MapGetLocationDTOwithEvents(Location entity, GetLocationDTO getlocationDTO)
        {
            var result = new List<GetSimpleEventDTO>();
            if (entity.EventsList == null)
            {
                return result;
            }
            foreach (var record in entity.EventsList)
            {
                result.Add(new GetSimpleEventDTO()
                {
                    Id = record.Id,
                    Name = record.Name,
                    Date = record.Date,
                    
                });
            }
            return result;
        }

        //---------------------------------------------------------------------------------------------------------------------
        private List<GetFavouriteDTO> MapGetUserDTOwithFavourites(User entity, GetUserDTO getlocationDTO)
        {
            var result = new List<GetFavouriteDTO>();
            if (entity.Favourites == null)
            {
                return result;
            }
            foreach (var record in entity.Favourites)
            {
                result.Add(new GetFavouriteDTO()
                {
                    Id = record.Id,
                    CreatedAt = record.CreatedAt,
                    EventId = record.EventId,
                    UserId  = record.UserId,
                    
                });
            }
            return result;
        }

        private List<GetFollowDTO> MapGetUserDTOwithFollowers(User entity, GetUserDTO getlocationDTO)
        {
            var result = new List<GetFollowDTO>();
            if (entity.Followers == null)
            {
                return result;
            }
            foreach (var record in entity.Followers)
            {
                result.Add(new GetFollowDTO()
                {
                    Id = record.Id,
                    CreatedAt= record.CreatedAt,
                    AdminId = record.AdminId,
                    UserId= record.UserId,

                });
            }
            return result;
        }

        private List<GetFollowDTO> MapGetUserDTOwithFollowing(User entity, GetUserDTO getlocationDTO)
        {
            var result = new List<GetFollowDTO>();
            if (entity.Following == null)
            {
                return result;
            }
            foreach (var record in entity.Following)
            {
                result.Add(new GetFollowDTO()
                {
                    Id = record.Id,
                    CreatedAt =  record.CreatedAt,
                    AdminId= record.AdminId,
                    UserId=record.UserId,

                });
            }
            return result;
        }

        private List<GetFormDTO> MapGetUserDTOwithForms(User entity, GetUserDTO getlocationDTO)
        {
            var result = new List<GetFormDTO>();
            if (entity.FormResponses == null)
            {
                return result;
            }
            foreach (var record in entity.FormResponses)
            {
                result.Add(new GetFormDTO()
                {
                    Id = record.Id,
                    CreatedAt = record.CreatedAt,
                    Comment= record.Comment,
                    EventId= record.EventId,
                    UserId = record.UserId,

                });
            }
            return result;
        }
        private List<GetSimpleTicketDTO> MapGetUserDTOwithTickets(User entity, GetUserDTO getlocationDTO)
        {
            var result = new List<GetSimpleTicketDTO>();
            if (entity.Tickets == null)
            {
                return result;
            }
            foreach (var record in entity.Tickets)
            {
                result.Add(new GetSimpleTicketDTO()
                {
                    Id = record.Id,
                    TicketPrice = record.TicketPrice,
                    UserId = record.UserId,
                    EventId = record.EventId,
                    CouponId = record.CouponId,

                });
            }
            return result;
        }
        
    }
}
