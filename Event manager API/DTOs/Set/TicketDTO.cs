using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Event_manager_API.DTOs.Set
{
    public class TicketDTO
    {
        


        //RELATIONSHIPS

        //------User
        [Required]
        public int UserId { get; set; }
        

        //------Event
        [Required]
        public int EventId { get; set; }
        
        
        //------Coupon
        public int CouponId { get; set; }
        
    }
}
