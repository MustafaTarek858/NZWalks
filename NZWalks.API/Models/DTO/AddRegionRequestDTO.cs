﻿using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class AddRegionRequestDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code has to be Minimum of 3 characters")]
        [MaxLength(3, ErrorMessage = "Code has to be Maximum of 3 characters")]
        public string Name { get; set; } 

        [Required]
        [MaxLength(100, ErrorMessage = "Code has to be Maximum of 100 characters")]
        public string Code { get; set; } 

        public string? RegionImageUrl { get; set; }
    }
}
