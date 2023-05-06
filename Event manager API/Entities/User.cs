using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace Event_manager_API.Entities
{
    public class User
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }


        //RELATIONSHIPS

        //------X


        //LISTS

        //------Tickets
        public List<Ticket> Tickets { get; set; }

        //------FormResponses
        public List<Form> FormResponses { get; set; }

        //------Follows
        [InverseProperty(nameof(Follow.Admin))]
        public List<Follow> Following { get; set; }

        [InverseProperty(nameof(Follow.User))]
        public List<Follow> Followers { get; set; }

        //------Favourites
        public List<Favourite> Favourites { get; set; }
    }
}
