using System.Collections.Generic;
using NUnit.Framework;
using BusinessEntities;
using ProjectManagerApi.Controllers;
using Moq;
using DataAccessLayer;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BusinessLayer;


namespace ProjectManagerApi.Tests
{
    [TestFixture]
    public class UserControllerTest
    {
        private ProjectManagerContext CreateContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProjectManagerContext>();
            optionsBuilder.UseInMemoryDatabase("ProjectManagerTestUserDB").UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            return new ProjectManagerContext(optionsBuilder.Options);
        }

        [TearDown]
        public void CleanUp()
        {
            DeleteDB();
        }

        [Test]
        public void AddUserTest()
        {
            var result = AddUser(MockFirstUser());
            Assert.AreEqual(result, true);
        }

        [Test]
        public void UpdateUserTest()
        {
            AddUser(MockFirstUser());
            var userId = GetFirstUser().UserId;

            var user = MockFirstUser();
            user.UserId = userId;
            user.LastName = "Abdul";
            var result = UpdateUser(user);
            Assert.AreEqual(result, true);

            var updatedUser = GetFirstUser();
            Assert.AreEqual(updatedUser.LastName, "Abdul");
        }

        [Test]
        public void GetUserTest()
        {
            AddUser(MockFirstUser());
            var userId = GetFirstUser().UserId;
            var user = GetUser(userId);
            Assert.AreEqual(userId, user.UserId);
        }

        [Test]
        public void GetAllUsersTest()
        {
            DeleteDB();
            AddUser(MockFirstUser());
            AddUser(MockSecondUser());

            var result = GetAllUsers();
            Assert.AreEqual(result.Any(), true);
            Assert.AreEqual(result.Count, 2);
        }

        private User MockFirstUser()
        {
            User user = new User();
            user.UserId = 0;
            user.FirstName = "Thiyagu";
            user.LastName = "Alagu";
            user.EmployeeId = 596558;
            user.IsDeleted = false;
            return user;
        }
        private User MockSecondUser()
        {
            User user = new User();
            user.UserId = 0;
            user.FirstName = "mahesh";
            user.LastName = "V";
            user.EmployeeId = 652874;
            user.IsDeleted = false;
            return user;
        }


        private bool AddUser(User user)
        {
            using (ProjectManagerContext _dbContext = CreateContext())
            {
                IUserRepository _repo = new UserRepository(_dbContext);
                var controller = new UserController(_repo);
                return controller.AddUser(user);
            }
        }

        private bool UpdateUser(User user)
        {
            using (ProjectManagerContext _dbContext = CreateContext())
            {
                IUserRepository _repo = new UserRepository(_dbContext);
                var controller = new UserController(_repo);
                return controller.UpdateUser(user);
            }
        }

        private User GetFirstUser()
        {
            using (ProjectManagerContext _dbContext = CreateContext())
            {
                IUserRepository _repo = new UserRepository(_dbContext);
                var controller = new UserController(_repo);
                return controller.GetAllUsers().First();
            }
        }

        private User GetUser(int id)
        {
            using (ProjectManagerContext _dbContext = CreateContext())
            {
                IUserRepository _repo = new UserRepository(_dbContext);
                var controller = new UserController(_repo);
                return controller.GetUser(id);
            }
        }

        private List<User> GetAllUsers()
        {
            using (ProjectManagerContext _dbContext = CreateContext())
            {
                IUserRepository _repo = new UserRepository(_dbContext);
                var controller = new UserController(_repo);
                return controller.GetAllUsers();
            }
        }

        private void DeleteDB()
        {
            using (ProjectManagerContext _dbContext = CreateContext())
            {
                _dbContext.Database.EnsureDeleted();
            }
        }
    }
}
