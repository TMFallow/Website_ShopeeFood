using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
                        return View();
                    }
                }
                StringContent content = new StringContent(JsonConvert.SerializeObject(users),
                    Encoding.UTF8, "application/json");

                HttpResponseMessage message1 = await client.PostAsync("api/users/UpdateUser", content);
            }
            ViewData["updateMessage"] = "Updated Successfully";
            return View();
        }

        [HttpGet]
        public IActionResult UpdateUserAddress()
        {
            ViewBag.checkUpdateAddress = TempData["updateAddressUser"] as string;
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> AddressUserInfo_Partial()
        {
            List<AddressUserModel> addressUser = new List<AddressUserModel>();

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

                    ViewData["InfoUser"] = new UsersModel()
                    {
                        UserId = users.UserId,
                        Username = users.Username,
                        Password = users.Password,
                        Email = users.Email,
                        FullName = users.FullName,
                        Sex = users.Sex,
                        PhoneNumber = users.PhoneNumber,
                        Image = users.Image,
                        Token = users.Token,
                    };
                }

                if (users != null)
                {
                    int? userId = users.UserId;

                    HttpContext.Session.SetString("UserAddressId", userId.ToString());

                    HttpResponseMessage message1 = await client.GetAsync("api/address/getListAddressByID/" + userId + "");

                    if (message1.IsSuccessStatusCode)
                    {
                        var addressUserMessage = message1.Content.ReadAsStringAsync().Result;

                        addressUser = JsonConvert.DeserializeObject<List<AddressUserModel>>(addressUserMessage).ToList();
                    }
                }
            }

            return PartialView("AddressUserInfo_Partial", addressUser);
        }

        [HttpGet]
        public async Task<JsonResult> EditUserAddress(int? Id)
        {
            AddressUserModel addressUser = new AddressUserModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));


                HttpResponseMessage message1 = await client.GetAsync("api/address/GetAddressToDelivery/" + Id + "");

                if (message1.IsSuccessStatusCode)
                {
                    var addressUserMessage = message1.Content.ReadAsStringAsync().Result;

                    addressUser = JsonConvert.DeserializeObject<AddressUserModel>(addressUserMessage);
                }
            }
            return Json(addressUser);
        }

        public async Task<IActionResult> EditUserAddressByID(int Id)
        {
            AddressUserModel addressUser = new AddressUserModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));


                HttpResponseMessage message1 = await client.GetAsync("api/address/GetAddressToDelivery/" + Id + "");

                if (message1.IsSuccessStatusCode)
                {
                    var addressUserMessage = message1.Content.ReadAsStringAsync().Result;

                    addressUser = JsonConvert.DeserializeObject<AddressUserModel>(addressUserMessage);
                }
            }
            return PartialView("_UpdateAddressInfo", addressUser);
        }

        [HttpPost]
        public async Task<IActionResult> UpadteAddress(string name, string address, string email, string phoneNumber, string areas, string detailAreas, string nameUser)
        {
            AddressUserModel addressUserModel = new AddressUserModel();

            if(name != null)
            {
                addressUserModel.Name = name;
            }
            if (address != null)
            {
                addressUserModel.Address = address;
            }
            if (email != null)
            {
                addressUserModel.Email = email;
            }
            if (phoneNumber != null)
            {
                addressUserModel.PhoneNumbers = phoneNumber;
            }
            if (areas != null)
            {
                addressUserModel.areas = areas;
            }
            if (detailAreas != null)
            {
                addressUserModel.detailAreas = detailAreas;
            }
            if (nameUser != null)
            {
                addressUserModel.nameUser = nameUser;
            }
            if(HttpContext.Session.GetString("UserAddressId") !=null)
            {
                addressUserModel.UserID = int.Parse(HttpContext.Session.GetString("UserAddressId"));
            }    

            StringContent content = new StringContent(JsonConvert.SerializeObject(addressUserModel),
                    Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));


                HttpResponseMessage message1 = await client.PostAsync("api/Address/UpdateAddressUser/", content);

                if (message1.IsSuccessStatusCode)
                {
                    TempData["updateAddressUser"] = "Updated Succesfully";
                }
            }

            TempData["updateAddressUser"] = "Please Try Again";

            return RedirectToAction("UpdateUserAddress");
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
