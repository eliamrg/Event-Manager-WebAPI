﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Event_manager_API.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public DateTime createdAt { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TicketPrice { get; set; } //Can or not have discount depending if user has a coupon
        public int Quantity { get; set; }

        //RELATIONSHIPS

        //------User
        public int UserId { get; set; }
        public User User { get; set; }

        //------Event
        public int EventId { get; set; }
        public Event Event { get; set; }
        
        //------Coupon
        public int CouponId { get; set; }
        public Coupon Coupon { get; set; }

        


        //LISTS

        //------X
    }
}