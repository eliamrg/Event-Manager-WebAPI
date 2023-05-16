using Event_manager_API.Validations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace Event_manager_API.Entities
{
    public class Event
    {
        public int Id { get; set; }
        
        public DateTime CreatedAt { get; set; }
        [Required] 
        [FirstLetterUppercase]
        public string Name { get; set; }
        
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TicketPrice { get; set; }
        [Required]
        [CapacityNotCero]
        public int EventCapacity { get; set; }
        [Required]
        public DateTime Date { get; set; }



        //RELATIONSHIPS

        //------User(Admin)
        [Required]
        public int AdminId { get; set; }
        public ApplicationUser Admin { get; set; }

        //------Location
        [Required]
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
