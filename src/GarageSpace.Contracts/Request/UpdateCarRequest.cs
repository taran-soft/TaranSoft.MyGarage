﻿namespace GarageSpace.Contracts.Request;

public class UpdateCarRequest
{
    public string Model { get; set; }
    public string Year { get; set; }
    public Guid ImageId { get; set; }
}