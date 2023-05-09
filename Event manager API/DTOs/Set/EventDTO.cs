using Event_manager_API.Validations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace Event_manager_API.DTOs.Set
{
    public class EventDTO
    {
        
        [Required]
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
       

        //------Location
        [Required]
        public int LocationId { get; set; }
        
        
        
        
    }
}
