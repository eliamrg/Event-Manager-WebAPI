using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace Event_manager_API.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TicketPrice { get; set; }
        public int EventCapacity { get; set; }
        public DateTime Date { get; set; }



        //RELATIONSHIPS

        //------User(Admin)
        public int AdminId { get; set; }
        public User Admin { get; set; }
        
        //------Location
        public int LocationId { get; set; }
        public Location Location { get; set; }
        
        //LISTS

        //------Tickets
        public List<Ticket> Tickets { get; set; }
        
        //------Coupons
        public List<Coupon> Coupons { get; set; }
        
        //------Form
        public List<Form> FormResponses { get; set; }
        
    }
}
