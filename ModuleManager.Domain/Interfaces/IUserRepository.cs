using ModuleManager.Domain;
using System.Collections.Generic;

namespace ModuleManager.UserDAL.Interfaces
{
    public interface IUserRepository
    {
        User GetOne(string key);

        IEnumerable<User> GetAll();

        bool Create(User entity);

        bool Edit(User entity);
    }
}