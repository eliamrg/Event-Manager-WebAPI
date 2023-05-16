using Event_manager_API.Validations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Event_manager_API.DTOs.Get
{
    public class GetSimpleCouponDTO
    {
        public int Id { get; set; }
       
 

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountPercentage { get; set; }
        [Required]
        public string Code { get; set; }

        //RELATIONSHIPS

        //------Event
        [Required]
        public int EventId { get; set; }
      
    }
}
