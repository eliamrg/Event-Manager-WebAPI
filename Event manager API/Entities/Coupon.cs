using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Event_manager_API.Entities
{
    public class Coupon
    {
        public int Id { get; set; }
        public DateTime createdAt { get; set; }

        public string Description { get; set; }
        public string Code { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountPercentage { get; set; }


        //RELATIONSHIPS

        //------Event
        public int EventId { get; set; }
        public Event Event { get; set; }


        //LISTS

        //------Tickets Using that Coupon
        public List<Ticket> Tickets { get; set; }
    }
}
