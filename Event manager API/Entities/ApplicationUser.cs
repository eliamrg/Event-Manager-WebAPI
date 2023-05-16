using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using Event_manager_API.Validations;
using Microsoft.AspNetCore.Identity;

namespace Event_manager_API.Entities
{
    public class ApplicationUser
    {
        
        public int Id { get; set; }


        //Link Account
        [Required]
        //[ForeignKey("AccountId")]
        public string AccountId { get; set; }
        public IdentityUser Account{ get; set; }

        
        public DateTime CreatedAt { get; set; }

        [Required] //
        [StringLength(maximumLength: 30, ErrorMessage = "Max Lnegth is 20 Characters")]
        [FirstLetterUppercase]
        public string Username { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        
        [Required]
        [ValidRole]
        public string Role { get; set; }


        //RELATIONSHIPS

        //------X


        //LISTS

        //------Tickets
        public List<Ticket> Tickets { get; set; }

        //------FormResponses
        public List<Form> FormResponses { get; set; }

        //------Follows
        
        public List<Follow> Following { get; set; }
        public List<Follow> Followers { get; set; }

        //------Favourites
        public List<Favourite> Favourites { get; set; }
    }
}
