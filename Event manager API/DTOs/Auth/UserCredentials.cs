﻿using System.ComponentModel.DataAnnotations;

namespace Event_manager_API.DTOs.Auth
{
    public class UserCredentials
    {
        [Required] 
        public string UserName { get; set;}
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
