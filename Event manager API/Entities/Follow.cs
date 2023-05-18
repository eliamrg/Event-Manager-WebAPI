using System.ComponentModel.DataAnnotations;

namespace Event_manager_API.Entities
{
    public class Follow
    {
        public int Id { get; set; }
       
        public DateTime CreatedAt { get; set; }


        //RELATIONSHIPS

        //------User
        [Required] 
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }

        //------User Admin
        [Required] 
        public int AdminId { get; set; }
        public ApplicationUser Admin { get; set; }




        //LISTS

        //------X
    }
}
