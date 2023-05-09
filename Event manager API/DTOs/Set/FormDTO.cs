using System.ComponentModel.DataAnnotations;

namespace Event_manager_API.DTOs.Set
{
    public class FormDTO
    {
        
        [Required] 
        public DateTime CreatedAt { get; set; }
        [Required]
        public int Comment { get; set; }

        //RELATIONSHIPS

        //------User
        [Required]
        public int UserId { get; set; }
        

        //------Event
        [Required]
        public int EventId { get; set; }
    }
}
