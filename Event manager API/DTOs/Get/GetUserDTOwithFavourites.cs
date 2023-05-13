using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using Event_manager_API.Validations;

namespace Event_manager_API.DTOs.Get
{
    public class GetUserDTOwithFavourites: GetUserDTO
    {
       
        //------Favourites
        public List<GetFavouriteDTO> Favourites { get; set; }
    }
}
