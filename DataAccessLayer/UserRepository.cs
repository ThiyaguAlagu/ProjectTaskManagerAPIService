using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Linq;

namespace DataAccessLayer
{
    public class UserRepository : IUserRepository
    {
        ProjectManagerContext _context;
        public UserRepository(ProjectManagerContext context)
        {
            _context = context;
        }
        public virtual List<User> GetAllUsers()
        {
            var query = from user in _context.Set<User>()
                        where user.IsDeleted==false
                        select user;
            var users = query.Include(t => t.UserProject).Include(t => t.UserTask).AsNoTracking().ToList();
            return users;
        }
        public virtual User GetUser(int id)
        {
            var query = from user in _context.Set<User>()
                        select user;
            var selectedUser = query.Include(t => t.UserProject).Include(t => t.UserTask).AsNoTracking().First();
            return selectedUser;
        }
        public virtual bool UpdateUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            _context.Set<User>().Update(user);
            _context.SaveChanges();
            return true;
        }
        public virtual bool AddUser(User user)
        {
            _context.Set<User>().Add(user);
            _context.SaveChanges();
            return true;
        }
    }
}

