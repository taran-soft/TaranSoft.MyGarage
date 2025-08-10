﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GarageSpace.Data.Models.EF
{
    [Table("Countries")]
    public class Country
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }
        public string? CountryCode { get; set; }

    }
}
