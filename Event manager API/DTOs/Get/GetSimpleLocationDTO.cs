using Event_manager_API.Validations;
using System.ComponentModel.DataAnnotations;

namespace Event_manager_API.DTOs.Get
{
    public class GetSimpleLocationDTO
    {
        public int Id { get; set; }
        
       
        
        [Required] 
        [FirstLetterUppercase]
        public string Name { get; set; }
        
        [Required]
        public string Address { get; set; }



        
    }
}
