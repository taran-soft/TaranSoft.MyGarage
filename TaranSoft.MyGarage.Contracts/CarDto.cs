﻿namespace TaranSoft.MyGarage.Contracts
{
    public class CarDto
    {
        public Guid Id { get; set; }

        public string Model { get; set; }

        public string Year { get; set; }

        public Guid CreatedBy { get; set; }

        public Guid ImageId { get; set; }
    }
}
