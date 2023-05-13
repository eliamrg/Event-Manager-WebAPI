using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using Event_manager_API.Validations;

namespace Event_manager_API.DTOs.Get
{
    public class GetUserDTOwithFollowing : GetUserDTO
    {
       
        //------Following
        public List<GetFollowDTO> Following { get; set; }
        
    }
}
