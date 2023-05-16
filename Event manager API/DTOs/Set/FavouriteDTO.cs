using System.ComponentModel.DataAnnotations;

namespace Event_manager_API.DTOs.Set
{
    public class FavouriteDTO
    {
        

        //RELATIONSHIPS

        ///------User
        [Required]
        public int UserId { get; set; }


        //------Event
        [Required]
        public int EventId { get; set; }

    }
}
