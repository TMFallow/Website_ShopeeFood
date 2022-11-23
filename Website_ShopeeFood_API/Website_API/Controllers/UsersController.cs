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

        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return users.GetAllUser().ToArray();
        }
    }
}
