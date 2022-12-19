using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Website_ShopeeFood.Models;
using Website_ShopeeFood.Services;

namespace Website_ShopeeFood.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IAPIServices aPIService;

        public static string[] listDistricts = RestaurantController.listDistricts;

        public HomeController(ILogger<HomeController> logger, IAPIServices aPIServices)
        {
            _logger = logger;
            this.aPIService = aPIServices;
        }

        //View Chưa Đăng Nhập Hien Thi DS San Pham
        public IActionResult Index()
        {
            var token = HttpContext.Session.GetString("JwToken");
            
            return View();
        }

        public IActionResult ReturnHomePage() 
        {
            RestaurantController.soLanThemQuan = 9;
            RestaurantController.checkingNumberofRestaurant = false;
            HttpContext.Session.SetString("CheckGetType", "");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ListOfRestaurant()
        {
            return View();
        }


        public async Task<RestaurantsModel> getRestaurantByIdType(int RestaurantID)
        {
            RestaurantsModel restaurants = new RestaurantsModel();

            using (var httpClient = new HttpClient())
            {
                configHttpClient(httpClient);

                HttpResponseMessage response = await httpClient.GetAsync("api/restaurant/GetListRestaurant/" + RestaurantID + "");

                if (response.IsSuccessStatusCode)
                {
                    var foodsTask = response.Content.ReadAsAsync<RestaurantsModel>();

                    foodsTask.Wait();

                    var dsfood = foodsTask.Result;

                    restaurants = dsfood;

                    return restaurants;
                }

                return null;
            }
        }

        public void configHttpClient(HttpClient httpClient)
        {
            string baseURL = aPIService.getIPAddress();

            httpClient.BaseAddress = new Uri(baseURL);

            httpClient.DefaultRequestHeaders.Clear();

            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        //HomePage Cho Dang Nhap Hien Thi San Pham
        public IActionResult HomePage()
        {
            return View();
        }

        [HttpGet]
        public IActionResult getAllList()
        {
            HttpContext.Session.SetString("CheckingList", "0");
            return RedirectToAction("ListOfRestaurant", "Home");
        }

        //Gọi Phương Thức Lấy ID
        [HttpGet]
        public IActionResult getIdType(int ID)
        {
            HttpContext.Session.SetString("CheckingList", "1");
            HttpContext.Session.SetString("IdTypeOfFood", ID.ToString());
            return RedirectToAction("ListOfRestaurant", "Home");
        }

        

        [HttpPost]
        public IActionResult filterRestaurantByDistricts(string[] list_districts)
        {
            HttpContext.Session.SetString("CheckingList", "2");
            
            listDistricts = list_districts;

            return RedirectToAction("ListOfRestaurant", "Home");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}