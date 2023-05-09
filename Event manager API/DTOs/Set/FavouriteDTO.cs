using System.ComponentModel.DataAnnotations;

namespace Event_manager_API.DTOs.Set
{
    public class FavouriteDTO
    {
        [Required]
        public DateTime CreatedAt { get; set; }

        //RELATIONSHIPS

        ///------User
        [Required]
        public int UserId { get; set; }


        //------Event
        [Required]
        public int EventId { get; set; }

    }
}
