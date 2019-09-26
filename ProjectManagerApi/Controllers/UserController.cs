using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessEntities;
using BusinessLayer;
using DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace ProjectManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repo;
        private readonly UserBl _userBl;
        public UserController(IUserRepository repo)
        {
            _repo = repo;
            _userBl = new UserBl(_repo);
        }
        [Route("GetAllUsers")]
        [HttpGet]
        public List<User> GetAllUsers()
        {
            return _userBl.GetAllUsers();
        }
        [Route("GetUser")]
        [HttpGet]
        public User GetUser(int id)
        {
            return _userBl.GetUser(id);
        }
        [Route("UpdateUser")]
        [HttpPost]
        public bool UpdateUser(User user)
        {
            return _userBl.UpdateUser(user);
        }
        [Route("AddUser")]
        [HttpPost]
        public bool AddUser(User user)
        {
            return _userBl.AddUser(user);
        }
    }
}