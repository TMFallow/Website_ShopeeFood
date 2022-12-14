using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Website_ShopeeFood.Models;
using Website_ShopeeFood.Services;

namespace Website_ShopeeFood.Controllers
{
    public class UserController : Controller
    {
        private readonly IAPIServices IAPIServices;

        private readonly string directoryImagePath;

        private readonly IWebHostEnvironment webHostEnvironment;

        public UserController(IAPIServices iAPIServices, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
            directoryImagePath = configuration["App_Setting:ImageDir"];
            this.IAPIServices = iAPIServices;
        }

        [HttpGet]
        public IActionResult UpdateUserInfo()
        {
            return View();
        }

        //Update User Info 

        [HttpPost]
        [ActionName("UpdateUserInfo")]
        public async Task<IActionResult> UpdateUserInfo(string fullName, string sex, string email, string password, string repassword)
        {
            UsersModel users = new UsersModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(IAPIServices.getIPAddress());

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
                        TempData["updateMessage"] = "Password Is Not Matched";
                        ViewBag.updateMessage = TempData["updateMessage"] as string;
                        return View();
                    }
                }
                StringContent content = new StringContent(JsonConvert.SerializeObject(users),
                    Encoding.UTF8, "application/json");

                HttpResponseMessage message1 = await client.PostAsync("api/users/UpdateUser", content);
            }
            TempData["updateMessage"] = "Updated Successfully";
            ViewBag.updatemsg = TempData["updateMessage"] as string;
            return View();
        }


        //Return View Update Address

        [HttpGet]
        public IActionResult UpdateUserAddress()
        {
            ViewBag.checkUpdateAddress = TempData["updateAddressUser"] as string;

            ViewBag.checkInsertAddress = TempData["insertAddressUser"] as string;

            ViewBag.checkDeleteAddress = TempData["DeleteAddressUser"] as string;

            return View();
        }

        //Get Address By Id Based On Username

        [HttpGet]
        public async Task<IActionResult> AddressUserInfo_Partial()
        {
            List<AddressUserModel> addressUser = new List<AddressUserModel>();

            UsersModel users = new UsersModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(IAPIServices.getIPAddress());

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

        //Return The Details Of Address

        static AddressUserModel addressUser = new AddressUserModel();

        [HttpGet]
        public async Task<IActionResult> EditUserAddress(int? Id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(IAPIServices.getIPAddress());

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage message1 = await client.GetAsync("api/address/GetAddressToDelivery/" + Id + "");

                HttpContext.Session.SetString("AddressIdForUser", Id.ToString());

                if (message1.IsSuccessStatusCode)
                {
                    var addressUserMessage = message1.Content.ReadAsStringAsync().Result;

                    addressUser = JsonConvert.DeserializeObject<AddressUserModel>(addressUserMessage);
                }
            }
            return RedirectToAction("UpdateUserAddress", addressUser);
        }
        
        //Get Address Of User Filtered By Id
        public async Task<IActionResult> EditUserAddressByID(int Id)
        {
            AddressUserModel addressUser = new AddressUserModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(IAPIServices.getIPAddress());

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


        // Update The Address For User (Note Lại Để Sửa Sau)
        [HttpPost]
        [Route("UpadteAddress")]
        public async Task<IActionResult> UpadteAddress(string name, string address, string email, string phoneNumber, string areas, string detailAreas, string nameUser)
        {
            AddressUserModel addressUserModel = new AddressUserModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(IAPIServices.getIPAddress());

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage addressMessage = await client.GetAsync("api/address/GetAddressToDelivery/" + HttpContext.Session.GetString("AddressIdForUser") + "");

                if (addressMessage.IsSuccessStatusCode)
                {
                    var addressUserMessage = addressMessage.Content.ReadAsStringAsync().Result;

                    addressUserModel = JsonConvert.DeserializeObject<AddressUserModel>(addressUserMessage);
                }


                if (name != addressUserModel.Name)
                {
                    addressUserModel.Name = name;
                }
                if (address != addressUserModel.Address)
                {
                    addressUserModel.Address = address;
                }
                if (email != addressUserModel.Email)
                {
                    addressUserModel.Email = email;
                }
                if (phoneNumber != addressUserModel.PhoneNumbers)
                {
                    addressUserModel.PhoneNumbers = phoneNumber;
                }
                if (areas != addressUserModel.areas)
                {
                    addressUserModel.areas = areas;
                }
                if (detailAreas != addressUserModel.detailAreas)
                {
                    addressUserModel.detailAreas = detailAreas;
                }
                if (nameUser != addressUserModel.nameUser)
                {
                    addressUserModel.nameUser = nameUser;
                }
                if (int.Parse(HttpContext.Session.GetString("UserAddressId")) != addressUserModel.UserID)
                {
                    addressUserModel.UserID = int.Parse(HttpContext.Session.GetString("UserAddressId"));
                }
                if (int.Parse(HttpContext.Session.GetString("AddressIdForUser")) != addressUserModel.ID)
                {
                    addressUserModel.ID = int.Parse(HttpContext.Session.GetString("AddressIdForUser"));
                }
            }

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(IAPIServices.getIPAddress());

                httpClient.DefaultRequestHeaders.Clear();

                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                StringContent contents = new StringContent(JsonConvert.SerializeObject(addressUserModel), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("api/address/UpdateAddressUser/", contents))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["updateAddressUser"] = "Updated Succesfully";
                    }
                    else
                    {
                        TempData["updateAddressUser"] = "Please Try Again";
                    }
                }
            }
            TempData["insertAddressUser"] = null;
            TempData["DeleteAddressUser"] = null;

            return RedirectToAction("UpdateUserAddress");
        }

        //Insert Address for User
        [HttpPost]
        public async Task<IActionResult> InsertUserAddress(AddressUserModel model)
        {
            model.UserID = int.Parse(HttpContext.Session.GetString("UserAddressId"));

            using (var client = new HttpClient())
            {

                StringContent content = new StringContent(JsonConvert.SerializeObject(model),
                   Encoding.UTF8, "application/json");

                HttpResponseMessage message1 = await client.PostAsync(IAPIServices.getIPAddress() + "api/address/InsertAddress", content);

                if (message1.IsSuccessStatusCode)
                {
                    TempData["insertAddressUser"] = "Updated Succesfully";
                }
                else
                {
                    TempData["insertAddressUser"] = "Please Try Again";
                }
            }
            TempData["updateAddressUser"] = null;
            TempData["DeleteAddressUser"] = null;


            return RedirectToAction("UpdateUserAddress");

        }

        //Delete Address of Users 
        [HttpGet]
        public async Task<IActionResult> DeleteUserAddress(int Id)
        {
            AddressUserModel addressModel = new AddressUserModel();

            using (var client = new HttpClient())
            {
                HttpResponseMessage getAddressMessage = await client.GetAsync(IAPIServices.getIPAddress() + "api/address/GetAddressToDelivery/" + Id + "");
                if(getAddressMessage.IsSuccessStatusCode)
                {
                    var addressUserMessage = getAddressMessage.Content.ReadAsStringAsync().Result;

                    addressModel = JsonConvert.DeserializeObject<AddressUserModel>(addressUserMessage);
                }

                if(addressModel != null)
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(addressModel),
                  Encoding.UTF8, "application/json");

                    HttpResponseMessage deleteAddressMessage = await client.PostAsync(IAPIServices.getIPAddress() + "api/address/DeleteAddress", content);

                    if (deleteAddressMessage.IsSuccessStatusCode)
                    {
                        TempData["DeleteAddressUser"] = "Delete Address Succesfully";
                    }
                    else
                    {
                        TempData["DeleteAddressUser"] = "Please Try Again";
                    }
                }
            }
            TempData["updateAddressUser"] = null;
            TempData["insertAddressUser"] = null;

            return View("UpdateUserAddress");
        }


        // Get Info Of User
        [HttpGet]
        public async Task<IActionResult> UserInfo_Partial()
        {
            UsersModel users = new UsersModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(IAPIServices.getIPAddress());

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
                client.BaseAddress = new Uri(IAPIServices.getIPAddress());

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

        public IActionResult _UserAddressInfo()
        {
            return PartialView("_UserAddressInfo", addressUser);
        }

        public async Task<IActionResult> uploadImageURL(string filename)
        {
            int userID = 0;

            userID = int.Parse(HttpContext.Session.GetString("UserIdToCheckInvoices"));

            if(userID == 0)
            {
                userID = 1;
            }

            UsersModel usersModel = new UsersModel();

            usersModel = await IAPIServices.getUserById(userID);

            if(usersModel != null)
            {
                string wwwRootPath = webHostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(filename);
                string extension = Path.GetExtension(filename);
                fileName = fileName + extension;
                string path = Path.Combine(wwwRootPath + "/image", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    usersModel.Image = fileStream.Name.ToString();
                }
            }
            return RedirectToAction("UpdateUserInfo");
        }
    }
}
