using Event_manager_API.Validations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace Event_manager_API.DTOs.Get
{
    public class GetEventDTOwithTickets: GetEventDTO
    {
        
        //------Tickets
        public List<GetSimpleTicketDTO> Tickets { get; set; }

        
    }
}
