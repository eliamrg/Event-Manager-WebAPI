using System.ComponentModel.DataAnnotations;

namespace Event_manager_API.DTOs.Get
{
    public class GetFollowDTO
    {
        public int Id { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }


        //RELATIONSHIPS

        //------User
        [Required] 
        public int UserId { get; set; }
        public GetSimpleUserDTO User { get; set; }

        //------User Admin
        [Required] 
        public int AdminId { get; set; }
        public GetSimpleUserDTO Admin { get; set; }




        //LISTS

        //------X
    }
}
