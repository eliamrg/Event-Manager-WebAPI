using System.ComponentModel.DataAnnotations;

namespace Event_manager_API.Entities
{
    public class Favourite
    {
        public int Id { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }


        //RELATIONSHIPS

        ///------User
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        //------Event
        [Required]
        public int EventId { get; set; }
        public Event Event { get; set; }


        //LISTS

        //------X
    }
}
