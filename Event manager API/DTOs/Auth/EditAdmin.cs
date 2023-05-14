using System.ComponentModel.DataAnnotations;

namespace Event_manager_API.DTOs.Auth
{
    public class EditAdmin
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
