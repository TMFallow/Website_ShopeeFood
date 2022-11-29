using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Website_ShopeeFood.Models;

namespace Website_ShopeeFood.Controllers
{
    public class UserController : Controller
    {
        string BaseUrl = "https://localhost:5001/";

        [HttpGet]
        public IActionResult UpdateUserInfo()
        {
            ViewBag.updatemsg = ViewData["updateMessage"] as string;
            return View();
        }

        [HttpPost]
        [ActionName("UpdateUserInfo")]
        public async Task<IActionResult> UpdateUserInfo(string fullName, string sex, string email, string password, string repassword)
        {
            UsersModel users = new UsersModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage message = await client.GetAsync("api/users/getUserByUsername/" + HttpContext.Session.GetString("UserName") + "");

                if (message.IsSuccessStatusCode)
                {
                    var usersMessage = message.Content.ReadAsStringAsync().Result;

                    users = JsonConvert.DeserializeObject<UsersModel>(usersMessage);
                }

                if (fullName != "")
                {
                    users.FullName = fullName;
                }

                if (sex != "")
                {
                    users.Sex = sex;
                }

                if (email != "")
                {
                    users.Email = email;
                }

                if (password != null || repassword != null)
                {
                    if (password == repassword)
                    {
                        users.Password = password;
                    }
                    else
                    {
                        ViewData["updateMessage"] = "Password Is Not Matched";
                        return View("UpdateUserInfo");
                    }
                }


                StringContent content = new StringContent(JsonConvert.SerializeObject(users),
                    Encoding.UTF8, "application/json");

                HttpResponseMessage message1 = await client.PostAsync("api/users/UpdateUser", content);

                ViewData["updateMessage"] = "Updated Successfully";

            }
            return View("UpdateUserInfo");
        }


        [HttpGet]
        public async Task<IActionResult> UserInfo_Partial()
        {
            UsersModel users = new UsersModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage message = await client.GetAsync("api/users/getUserByUsername/" + HttpContext.Session.GetString("UserName") + "");

                if (message.IsSuccessStatusCode)
                {
                    var usersMessage = message.Content.ReadAsStringAsync().Result;

                    users = JsonConvert.DeserializeObject<UsersModel>(usersMessage);
                }
            }
            return PartialView("UserInfo_Partial", users);
        }

        [HttpGet]
        public async Task<IActionResult> DetailUserInfo_Partial()
        {
            UsersModel users = new UsersModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage message = await client.GetAsync("api/users/getUserByUsername/" + HttpContext.Session.GetString("UserName") + "");

                if (message.IsSuccessStatusCode)
                {
                    var usersMessage = message.Content.ReadAsStringAsync().Result;

                    users = JsonConvert.DeserializeObject<UsersModel>(usersMessage);
                }
            }
            return PartialView("DetailUserInfo_Partial", users);
        }


    }
}
