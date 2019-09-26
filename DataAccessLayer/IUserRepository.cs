using System.Collections.Generic;
using BusinessEntities;

namespace DataAccessLayer
{
    public interface IUserRepository
    {
        bool AddUser(User user);
        List<User> GetAllUsers();
        User GetUser(int id);
        bool UpdateUser(User user);
    }
}
