﻿using Event_manager_API.Validations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Event_manager_API.DTOs.Set
{
    public class CouponDTO
    {
        
        

        public string Description { get; set; }
        
        [Required]
        
        public string Code { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountPercentage { get; set; }


        //RELATIONSHIPS

        //------Event
        [Required]
        public int EventId { get; set; }
        
    }
}
