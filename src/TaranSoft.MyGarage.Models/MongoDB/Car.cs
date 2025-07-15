﻿using TaranSoft.MyGarage.Data.Models.EF;

namespace TaranSoft.MyGarage.Data.Models.MongoDB
{
   // [Table("Cars")]
    public class Car : BaseEntity
    {
        public Manufacturer? ManufacturerId { get; set; }
        public string? Model { get; set; }

        public string? Year { get; set; }

        public EF.User? CreatedBy { get; set; }
        public EF.User? Owner { get; set; }

        public Guid? ImageId { get; set; }
    }
}
