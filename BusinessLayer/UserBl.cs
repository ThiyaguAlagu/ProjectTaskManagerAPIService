using BusinessEntities;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Text;


namespace BusinessLayer
{
    public class UserBl
    {
        private readonly IUserRepository _repo;
        public UserBl(IUserRepository repo)
        {
            _repo = repo;
        }
        public virtual List<User> GetAllUsers()
        {
            return _repo.GetAllUsers();
        }
        public virtual User GetUser(int id)
        {
            return _repo.GetUser(id);
        }
        public virtual bool UpdateUser(User user)
        {
            return _repo.UpdateUser(user);
        }
        public virtual bool AddUser(User user)
        {
            return _repo.AddUser(user);
        }
    }
}
