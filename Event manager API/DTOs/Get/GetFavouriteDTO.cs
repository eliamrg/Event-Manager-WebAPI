using System.ComponentModel.DataAnnotations;

namespace Event_manager_API.DTOs.Get
{
    public class GetFavouriteDTO
    {
        public int Id { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }


        //RELATIONSHIPS

        ///------User
        [Required]
        public int UserId { get; set; }
        public GetUserDTO User { get; set; }

        //------Event
        [Required]
        public int EventId { get; set; }
        public GetEventDTO Event { get; set; }


        //LISTS

        //------X
    }
}
