using System.ComponentModel.DataAnnotations;

namespace Event_manager_API.DTOs.Set
{
    public class FollowDTO
    {
        
        

        //RELATIONSHIPS

        //------User
        [Required] 
        public int UserId { get; set; }
        

        //------User Admin
        [Required] 
        public int AdminId { get; set; }
        
    }
}
