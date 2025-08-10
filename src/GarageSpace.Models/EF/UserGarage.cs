﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GarageSpace.Data.Models.EF.Vehicles;

namespace GarageSpace.Data.Models.EF;


[Table("UserGarages")]
public class UserGarage : BaseAuditableEntity
{
    [ForeignKey("Owner")]
    public long OwnerId { get; set; }
    public User Owner { get; set; }

    public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    public ICollection<Car> Cars => Vehicles.OfType<Car>().ToList();
    public ICollection<Motorcycle> Motorcycles => Vehicles.OfType<Motorcycle>().ToList();
    public ICollection<Trailer> Trailers => Vehicles.OfType<Trailer>().ToList();


}