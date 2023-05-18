using Event_manager_API.Validations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace Event_manager_API.DTOs.Get
{
    public class GetSimpleEventDTO
    {
        public int Id { get; set; }
        
        [Required] 
        [FirstLetterUppercase]
        public string Name { get; set; }
        
        public DateTime Date { get; set; }

        [Required]
        public int LocationId { get; set; }
        public GetSimpleLocationDTO Location { get; set; }

    }
}
