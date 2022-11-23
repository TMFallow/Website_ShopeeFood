using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System;
using Website_ShopeeFood.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Website_ShopeeFood.AppSetting;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using System.Net.WebSockets;

namespace Website_ShopeeFood.Controllers
{

    public class LoginController : Controller
    {
        string Baseurl = "https://localhost:5001/";

        private readonly IConfiguration configuration;



        public LoginController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Login_Users()
        {
            return PartialView();
        }

        public async Task<List<UsersModel>> GetListUser()
        {
            List<UsersModel> users = new List<UsersModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage message = await client.GetAsync("api/users");

                if (message.IsSuccessStatusCode)
                {
                    var usersMessage = message.Content.ReadAsStringAsync().Result;

                    var dsusers = JsonConvert.DeserializeObject<List<UsersModel>>(usersMessage);

                    foreach (var item in dsusers)
                    {
                        users.Add(item);
                    }

                    return users;
                }
            }
            return null;
        }

        [HttpPost]
        public async Task<IActionResult> Login_Users(string userName, string password)
        {
            var model = await GetListUser();
            UsersModel u = new UsersModel();

            foreach (var item in model)
            {
                if (item.Username == userName && item.Password == password) 
                {
                    u = item;
                }
            }

            using (var httpClient = new HttpClient())
            {
                string baseURL = "https://localhost:5001/api/Token/";

                StringContent content = new StringContent(JsonConvert.SerializeObject(u), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(baseURL, content))
                 {
                    string token = await response.Content.ReadAsStringAsync();
                    HttpContext.Session.SetString("JwToken", token);

                    var claims = new List<Claim>
                       {
                            new Claim(ClaimTypes.NameIdentifier, u.Username),
                       };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);

                    var pros = new AuthenticationProperties();

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, pros).Wait();


                    HttpContext.Session.SetString("ImageUser", u.Image);
                    HttpContext.Session.SetString("UserName", u.Username);
                    HttpContext.Session.SetString("Password", u.Password);
                    HttpContext.Session.SetString("FullName", u.FullName);

                    return RedirectToAction("Index", "Home");
                }
            }
        }

        //public void generateToken(UsersModel model)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(configuration["App_Setting:serectKey"]);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new Claim[]
        //        {
        //            new Claim(ClaimTypes.GivenName, model.FullName.ToString()),
        //            new Claim(ClaimTypes.Email, model.Email.ToString())
        //        }),
        //        Expires = DateTime.UtcNow.AddMinutes(5),

        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };

        //    var token = tokenHandler.CreateToken(tokenDescriptor);

        //    model.Token = tokenHandler.WriteToken(token);

        //    var cookieOptions = new CookieOptions
        //    {
        //        HttpOnly = true,
        //        Expires = DateTime.Now.AddMinutes(5)
        //    };
        //    Response.Cookies.Append("refreshToken", token.ToString(), cookieOptions);
        //}

        public IActionResult HomePage_Login_Partial()
        {   
            ViewBag.fullName = HttpContext.Session.GetString("FullName");
            ViewBag.imageUser = HttpContext.Session.GetString("ImageUser");

            return PartialView();
        }


        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
