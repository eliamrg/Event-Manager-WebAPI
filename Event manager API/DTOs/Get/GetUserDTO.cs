using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using Event_manager_API.Validations;
using Microsoft.AspNetCore.Identity;

namespace Event_manager_API.DTOs.Get
{
    public class GetUserDTO
    {
        public int Id { get; set; }

        [Required]
        public string AccountId { get; set; }
        public GetIdentityUserDTO Account { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required] //
        [StringLength(maximumLength: 30, ErrorMessage = "Max Lnegth is 30 Characters")]
        [FirstLetterUppercase]
        public string Username { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
       
        
        [Required]
        [ValidRole]
        public string Role { get; set; }

    }
}
