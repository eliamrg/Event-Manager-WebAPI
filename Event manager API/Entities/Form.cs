using System.ComponentModel.DataAnnotations;

namespace Event_manager_API.Entities
{
    public class Form
    {
        public int Id { get; set; }
        
        public DateTime CreatedAt { get; set; }
        [Required]
        public int Comment { get; set; }

        //RELATIONSHIPS

        //------User
        [Required]
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }

        //------Event
        [Required]
        public int EventId { get; set; }
        public Event Event { get; set; }


        //LISTS

        //------X
    }
}
