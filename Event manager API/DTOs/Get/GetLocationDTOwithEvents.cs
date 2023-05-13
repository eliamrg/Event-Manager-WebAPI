using Event_manager_API.Validations;
using System.ComponentModel.DataAnnotations;

namespace Event_manager_API.DTOs.Get
{
    public class GetLocationDTOwithEvents: GetLocationDTO
    {
        //------Events
        public List<GetSimpleEventDTO> EventsList { get; set; }

    }
}
