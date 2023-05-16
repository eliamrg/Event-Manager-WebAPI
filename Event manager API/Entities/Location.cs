using Event_manager_API.Validations;
using System.ComponentModel.DataAnnotations;

namespace Event_manager_API.Entities
{
    public class Location
    {
        public int Id { get; set; }
       
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
        public List<Event> EventsList { get; set; }


        
    }
}
