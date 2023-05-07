using System.ComponentModel.DataAnnotations;

namespace Event_manager_API.DTOs.Set
{
    public class FollowDTO
    {
        public int Id { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }


        //RELATIONSHIPS

        //------User
        [Required] 
        public int UserId { get; set; }
        public UserDTO User { get; set; }

        //------User Admin
        [Required] 
        public int AdminId { get; set; }
        public UserDTO Admin { get; set; }




        //LISTS

        //------X
    }
}
