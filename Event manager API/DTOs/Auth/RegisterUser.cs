using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using Event_manager_API.Validations;

namespace Event_manager_API.Entities
{
    public class RegisterUser
    {
  
        [Required] //
        [StringLength(maximumLength: 30, ErrorMessage = "Max Lnegth is 20 Characters")]
        [FirstLetterUppercase]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [PasswordPropertyText]
        public string Password { get; set; }

        [Required]
        [ValidRole]
        public string Role { get; set; }


       
    }
}
