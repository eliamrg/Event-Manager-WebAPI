using Event_manager_API.Validations;
using System.ComponentModel.DataAnnotations;

namespace Event_manager_API.DTOs.Set
{
    public class LocationDTO
    {
        public int Id { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        
        [Required] 
        [FirstLetterUppercase]
        public string Name { get; set; }
        
        [Required]
        public string Address { get; set; }

        [Required]
        [CapacityNotCero]
        public int Capacity { get; set; }


        //RELATIONSHIPS
        
        //------X
        
        
        //LISTS
        
        //------Events
        public List<EventDTO> EventsList { get; set; }


        
    }
}
