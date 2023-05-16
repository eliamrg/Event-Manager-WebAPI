using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Event_manager_API.DTOs.Get
{
    public class GetTicketDTO
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
        public GetUserDTO User { get; set; }

        //------Event
        [Required]
        public int EventId { get; set; }
        public GetEventDTO Event { get; set; }
        
        //------Coupon
        public int CouponId { get; set; }
        public GetCouponDTO Coupon { get; set; }

        


        //LISTS

        //------X
    }
}
