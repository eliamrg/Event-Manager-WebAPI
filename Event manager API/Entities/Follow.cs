﻿using System.ComponentModel.DataAnnotations;

namespace Event_manager_API.Entities
{
    public class Follow
    {
        public int Id { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }


        //RELATIONSHIPS

        //------User
        [Required] 
        public int UserId { get; set; }
        public User User { get; set; }

        //------User Admin
        [Required] 
        public int AdminId { get; set; }
        public User Admin { get; set; }




        //LISTS

        //------X
    }
}
