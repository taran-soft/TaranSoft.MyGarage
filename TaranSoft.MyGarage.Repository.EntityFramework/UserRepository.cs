using TaranSoft.MyGarage.Data.Models;
using TaranSoft.MyGarage.Repository.Interfaces;

namespace TaranSoft.MyGarage.Repository.EntityFramework
{
    public class UserRepository : IUserRepository
    {
        public Task<Guid> Create(User user)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByNickname(string name)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> ListAll()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Guid id, User user)
        {
            throw new NotImplementedException();
        }
    }
}
