using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Event_manager_API.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }

        
        [Column(TypeName = "decimal(18,2)")]
        public decimal TicketPrice { get; set; } //Can or not have discount depending if user has a coupon
        
       

        //RELATIONSHIPS

        //------User
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        //------Event
        [Required]
        public int EventId { get; set; }
        public Event Event { get; set; }
        
        //------Coupon
        public int CouponId { get; set; }
        public Coupon Coupon { get; set; }

        


        //LISTS

        //------X
    }
}
