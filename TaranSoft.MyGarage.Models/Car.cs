﻿using MongoDB.Bson.Serialization.Attributes;

namespace TaranSoft.MyGarage.Data.Models;

[BsonIgnoreExtraElements]
public class Car
{
    public Guid Id { get; set; }
    
    public string Model { get; set; }
    
    public string Year { get; set; }
    
    public Guid CreatedBy { get; set; }
    
    public Guid ImageId { get; set; }
    
}