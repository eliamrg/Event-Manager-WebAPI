using Event_manager_API.Validations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Event_manager_API.Entities
{
    public class Coupon
    {
        public int Id { get; set; }
        
        public DateTime CreatedAt { get; set; }

        public string Description { get; set; }
        
        [Required]
        public string Code { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountPercentage { get; set; }


        //RELATIONSHIPS

        //------Event
        [Required]
        public int EventId { get; set; }
        public Event Event { get; set; }


        //LISTS

        //------Tickets Using that Coupon
        public List<Ticket> Tickets { get; set; }
    }
}
