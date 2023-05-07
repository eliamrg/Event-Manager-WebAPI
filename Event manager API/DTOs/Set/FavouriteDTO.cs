using System.ComponentModel.DataAnnotations;

namespace Event_manager_API.DTOs.Set
{
    public class FavouriteDTO
    {
        public int Id { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }


        //RELATIONSHIPS

        ///------User
        [Required]
        public int UserId { get; set; }
        public UserDTO User { get; set; }

        //------Event
        [Required]
        public int EventId { get; set; }
        public EventDTO Event { get; set; }


        //LISTS

        //------X
    }
}
