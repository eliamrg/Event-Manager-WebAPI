﻿using System.ComponentModel.DataAnnotations;

namespace Event_manager_API.DTOs.Get
{
    public class GetFavouriteDTO
    {
        public int Id { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }


        //RELATIONSHIPS

        ///------User
        [Required]
        public int UserId { get; set; }
        public GetSimpleUserDTO User { get; set; }

        //------Event
        [Required]
        public int EventId { get; set; }
        public GetSimpleEventDTO Event { get; set; }


        //LISTS

        //------X
    }
}
