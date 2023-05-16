﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using Event_manager_API.Validations;

namespace Event_manager_API.DTOs.Get
{
    public class GetUserDTOwithTickets : GetUserDTO
    {
       
        //------Tickets
        public List<GetSimpleTicketDTO> Tickets { get; set; }

    }
}
