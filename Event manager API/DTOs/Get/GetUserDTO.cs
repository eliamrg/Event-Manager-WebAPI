using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using Event_manager_API.Validations;

namespace Event_manager_API.DTOs.Get
{
    public class GetUserDTO
    {
        public int Id { get; set; }
        
        [Required]
        public DateTime CreatedAt { get; set; }

        [Required] //
        [StringLength(maximumLength: 30, ErrorMessage = "Max Lnegth is 20 Characters")]
        [FirstLetterUppercase]
        public string Username { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [PasswordValidation]
        public string Password { get; set; }
        
        [Required]
        [ValidRole]
        public string Role { get; set; }


        //RELATIONSHIPS

        //------X


        //LISTS

        //------Tickets
        //public List<GetTicketDTO> Tickets { get; set; }

        //------FormResponses
        //public List<GetFormDTO> FormResponses { get; set; }

        //------Follows

        //public List<GetFollowDTO> Following { get; set; }
        //public List<GetFollowDTO> Followers { get; set; }

        //------Favourites
        //public List<GetFavouriteDTO> Favourites { get; set; }
    }
}
