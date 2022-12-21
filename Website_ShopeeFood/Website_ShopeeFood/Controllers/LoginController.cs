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
using System.Net;
using System.Net.Mail;
using System.Drawing;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Hosting;
using Website_ShopeeFood.Services;

namespace Website_ShopeeFood.Controllers
{

    public class LoginController : Controller
    {
        private readonly IConfiguration configuration;

        private readonly IAPIServices aPIServices;

        static AddressUserModel addressUsers = new AddressUserModel();

        public LoginController(IConfiguration configuration, IAPIServices aPIServices)
        {
            this.configuration = configuration;
            this.aPIServices = aPIServices;
        }

        [HttpGet]
        public IActionResult Login_Users()
        {
            ViewBag.message = TempData["message"] as string;
            ViewBag.check_Login = ViewData["Check_Login"] as string;
            return PartialView();
        }

        //public async Task<List<UsersModel>> GetListUser()
        //{
        //    List<UsersModel> users = new List<UsersModel>();

        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(Baseurl);

        //        client.DefaultRequestHeaders.Clear();

        //        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        //        HttpResponseMessage message = await client.GetAsync("api/users/GetUsers");

        //        if (message.IsSuccessStatusCode)
        //        {
        //            var usersMessage = message.Content.ReadAsStringAsync().Result;

        //            var dsusers = JsonConvert.DeserializeObject<List<UsersModel>>(usersMessage);

        //            foreach (var item in dsusers)
        //            {
        //                users.Add(item);
        //            }

        //            return users;
        //        }
        //    }
        //    return null;
        //}

        [HttpPost]
        public async Task<IActionResult> Login_Users(string userName, string password)
        {
            var model = await aPIServices.GetListUsers();
            UsersModel u = new UsersModel();

            int check = 0;

            foreach (var item in model)
            {
                if (item.Email == userName && item.Password == password)
                {
                    u = item;
                    check = 1;
                }
            }

            if (check == 1)
            {

                using (var httpClient = new HttpClient())
                {
                    string baseURL = aPIServices.getIPAddress();

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
                        HttpContext.Session.SetString("UserIdToCheckInvoices", u.UserId.ToString());

                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                ViewData["Check_Login"] = "Wrong Username or Password";
                return View();
            }
        }

        static List<AddressUserModel> listAddressUsers = new List<AddressUserModel>();
        
        public async Task<IActionResult> _AddressUser()
        {
            HttpContext.Session.SetString("CheckSearch", "0");

            int IdUser = int.Parse(HttpContext.Session.GetString("UserIdToCheckInvoices"));

            listAddressUsers = await aPIServices.getListAddressUserByUserId(IdUser);

            if(HttpContext.Session.GetString("nameAddress") != null)
            {
                listAddressUsers = aPIServices.searchListAddressUserModel(HttpContext.Session.GetString("nameAddress"), listAddressUsers);
            }

            return PartialView("_AddressUser", listAddressUsers);
        }

        public void searchAddress(string nameAddress)
        {
            if (nameAddress != null)
            {
                HttpContext.Session.SetString("nameAddress", nameAddress);
            }
            else
            {
                HttpContext.Session.Remove("nameAddress");
            }
        }

        public async Task<IActionResult> _HeaderUserAddress(int idAddress)
        {
            return PartialView("_HeaderUserAddress", addressUsers);
        }

        public async Task<IActionResult> getIdAddress(int idAddress)
        {
            addressUsers = await aPIServices.getAddressToDelivery(idAddress);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult HomePage_Login_Partial()
        {
            ViewBag.fullName = HttpContext.Session.GetString("FullName");
            ViewBag.imageUser = HttpContext.Session.GetString("ImageUser");
            return PartialView();
        }

        [HttpGet]
        public IActionResult Change_Password()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Change_Password(string email)
        {
            UsersModel model = new UsersModel();

            if (email == "")
            {
                return BadRequest();
            }

            model = await aPIServices.GetUsersByEmail(email);

            if (model != null)
            {
                string check = await SendMail("thanhbinh07102001@gmail.com", email, "thanhbinh07102001@gmail.com", "hdaxpupfiarzaejx", model.FullName);

                TempData["Email"] = "Sent Email Successfully";

                ViewBag.emailUser = TempData["Email"] as string;

                return View();
            }
            else
            {
                ViewBag.emailUser = null;
                return View();
            }
        }

        public static async Task<string> SendMail(string from, string to, string gmail, string password, string name)
        {
            MailMessage mailMessage = new MailMessage(from, to);

            mailMessage.Subject = "Change Password - Đổi Mật Khẩu Người Dùng ShopeeFood";

            mailMessage.Body = string.Format("Xin chào {0} <br />Vui lòng truy cập đường link dưới dây để cập nhật lại mật khẩu của bạn: http://localhost:44506/Login/ConfirmPassword/ <br />Xin cám ơn!", name);

            mailMessage.IsBodyHtml = true;

            mailMessage.ReplyToList.Add(new MailAddress(from));

            mailMessage.Sender = new MailAddress(from);

            using var smtp = new SmtpClient("smtp.gmail.com");

            smtp.Port = 587;

            smtp.UseDefaultCredentials = false;

            smtp.Credentials = new NetworkCredential(gmail, password);

            smtp.EnableSsl = true;

            try
            {
                await smtp.SendMailAsync(mailMessage);
                return "Sent Mail Successfully - Đã Gửi Mail";
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return  ex.Message;
            }
        }

        [HttpGet]
        public IActionResult ConfirmPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmPassword(string username, string password, string confirmPassword)
        {

            UsersModel usersModel = new UsersModel();

            string baseURL = aPIServices.getIPAddress();

            using (var client = new HttpClient())
            {
                usersModel = await aPIServices.GetUsersByEmail(username);

                if (usersModel != null)
                {
                    if (password == confirmPassword)
                    {
                        usersModel.Password = password;
                    }
                }
                client.BaseAddress = new Uri(baseURL);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(usersModel),
                    Encoding.UTF8, "application/json");

                HttpResponseMessage message2 = await client.PostAsync("api/users/UpdateUser", content);

                if (message2.IsSuccessStatusCode)
                {
                    var userTask = message2.Content.ReadAsAsync<UsersModel>();

                    userTask.Wait();

                    usersModel = userTask.Result;
                }
            }
            return RedirectToAction("Login_Users", "Login");
        }

        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.checkUSer = ViewData["checkUser"];
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string username, string password, string confirmPassword, string fullName, string sex, string email, string phoneNumber)
        {
            List<UsersModel> usersModel = new List<UsersModel>();

            UsersModel model = new UsersModel();

            using (var client = new HttpClient())
            {
                usersModel = await aPIServices.GetListUsers();

                foreach (var item in usersModel)
                {
                    if (email == item.Email || username == item.Username)
                    {
                        ViewData["checkUser"] = "User Is Already Exisit";
                        return View();
                    }
                }

                if(password != confirmPassword)
                {
                    ViewData["checkUser"] = "Password Is Not Matched";
                    return View();
                }

                model.Username = username;
                model.Password = password;
                model.FullName = fullName;
                model.Sex = sex;
                model.Email = email;
                model.PhoneNumber = phoneNumber;
                model.Image = "";
                model.Token = "";

                StringContent content = new StringContent(JsonConvert.SerializeObject(model),
                    Encoding.UTF8, "application/json");

                HttpResponseMessage message1 = await client.PostAsync("api/users/InsertUser", content);

                TempData["message"] = "Đăng Ký Thành Công";

                return RedirectToAction("Login_Users");
            }
        }


        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync();
            HttpContext.Session.Clear();
            addressUsers.ID = null;
            addressUsers.Name = null;
            addressUsers.Address = null;
            addressUsers.PhoneNumbers = null;
            addressUsers.UserID = null;
            addressUsers.areas = null;
            addressUsers.detailAreas = null;
            addressUsers.nameUser = null;
            addressUsers.Email = null;
            return RedirectToAction("Index", "Home");
        }

        
    }
}
