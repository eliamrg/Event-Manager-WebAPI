﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Event_manager_API.DTOs.Set
{
    public class TicketDTO
    {
        public int Id { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TicketPrice { get; set; } //Can or not have discount depending if user has a coupon
        
        [Required]
        public int Quantity { get; set; }

        //RELATIONSHIPS

        //------User
        [Required]
        public int UserId { get; set; }
        public UserDTO User { get; set; }

        //------Event
        [Required]
        public int EventId { get; set; }
        public EventDTO Event { get; set; }
        
        //------Coupon
        public int CouponId { get; set; }
        public CouponDTO Coupon { get; set; }

        


        //LISTS

        //------X
    }
}