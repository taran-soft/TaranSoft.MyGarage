using MongoDB.Driver;
using TaranSoft.MyGarage.Data.Models.MongoDB;

namespace TaranSoft.MyGarage.Repository.MongoDB.DbContext;

public interface IMongoDbContext
{
    IMongoCollection<User> Users { get; }
    IMongoCollection<Car> Cars { get; }
}