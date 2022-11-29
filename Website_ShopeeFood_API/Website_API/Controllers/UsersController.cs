using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopeeFood_Services.IServices;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Website_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUser_Services users;

        private readonly ILogger<UsersController> logger;

        public UsersController(IUser_Services users, ILogger<UsersController> logger)
        {
            this.users = users;
            this.logger = logger;
        }

        [HttpGet("GetUsers")]
        public IEnumerable<User> GetUsers()
        {
            return users.GetAllUser().ToArray();
        }

        [Route("InsertUser")]
        [HttpPost("InsertUser")]
        public void InsertUser(User user)
        {
            users.Insert(user);
        }

        [HttpPost("UpdateUser")]
        public void UpdateUser(User userModel)
        {
            users.Update(userModel);
        }

        [HttpGet("getUserByEmail/{email}")]
        public User getUserByEmail(string email)
        {
            return users.getUserByEmail(email);
        }

        [HttpGet("getUserByUsername/{username}")]
        public User getUserByUsername(string username)
        {
            return users.getUserByUsername(username);
        }
    }
}
