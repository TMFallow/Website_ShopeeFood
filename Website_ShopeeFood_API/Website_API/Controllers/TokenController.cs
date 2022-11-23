using Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShopeeFood_Repository;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Website_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration configuration;

        private readonly Application_Context context;

        public TokenController(IConfiguration configuration, Application_Context context)
        {
            this.configuration = configuration;
            this.context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post(User users)
        {
            var user = await GetUser(users.Username, users.Password);
            if (user != null)
            {
                var Claims = new[]
                {
                        new Claim("Id", user.UserId.ToString()),
                        new Claim("userName", user.Username),
                        new Claim("password", user.Password)
                    };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    configuration["Jwt:Issuer"],
                    configuration["Jwt:Audience"],
                    Claims,
                    expires: DateTime.UtcNow.AddMinutes(5),
                    signingCredentials: signIn
                    );

                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            else
            {
                return BadRequest("Invalid Credentials");
            }
        }

        [HttpGet]
        public async Task<User> GetUser(string userName, string password)
        {
            return await context.users.FirstOrDefaultAsync(x=>x.Username == userName && x.Password == password);
        }
    }
}
