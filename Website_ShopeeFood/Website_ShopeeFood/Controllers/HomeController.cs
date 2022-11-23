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

namespace Website_ShopeeFood.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public string Baseurl = "https://localhost:5001/";

        public static string[] listDistricts = RestaurantController.listDistricts;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //View Chưa Đăng Nhập Hien Thi DS San Pham
        public IActionResult Index()
        {
            var token = HttpContext.Session.GetString("JwToken");
            return View();
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
            string baseURL = "https://localhost:5001/";

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


//int check = int.Parse(HttpContext.Session.GetString("CheckingList"));
//if (check == 0)
//{
//    List<RestaurantsModel> restaurant = new List<RestaurantsModel>();

//    using (var client = new HttpClient())
//    {
//        client.BaseAddress = new Uri(Baseurl);

//        client.DefaultRequestHeaders.Clear();

//        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

//        HttpResponseMessage message = await client.GetAsync("api/restaurant/GetRestaurant");

//        if (message.IsSuccessStatusCode)
//        {
//            var restaurantMessage = message.Content.ReadAsStringAsync().Result;

//            restaurant = JsonConvert.DeserializeObject<List<RestaurantsModel>>(restaurantMessage).ToList();

//            List<RestaurantsModel> listRes = new List<RestaurantsModel>();

//            foreach (var item in restaurant)
//            {
//                if (item.AreaID == int.Parse(HttpContext.Session.GetString("AreaIDofRestaurant")))
//                {
//                    listRes.Add(item);
//                }
//            }

//            return View("ListOfRestaurant", listRes);
//        }
//    }
//}
//else if(check == 1)
//{
//    List<FoodModel> listOfFoods = new List<FoodModel>();

//    List<RestaurantsModel> listRestaurants = new List<RestaurantsModel>();

//    int ID = int.Parse(HttpContext.Session.GetString("IdTypeOfFood"));

//    using (var httpClient = new HttpClient())
//    {
//        configHttpClient(httpClient);

//        HttpResponseMessage response = await httpClient.GetAsync("api/food/getListRestaurantBasedOnTypeId/" + ID + "");

//        if (response.IsSuccessStatusCode)
//        {
//            var foodsTask = response.Content.ReadAsAsync<List<FoodModel>>();

//            foodsTask.Wait();

//            var dsfood = foodsTask.Result;

//            if (dsfood.Count != 0)
//            {
//                listOfFoods.Add(dsfood[0]);

//                for (int i = 1; i < dsfood.Count; i++)
//                {
//                    for (int j = i + 1; j < dsfood.Count - 1; j++)
//                    {
//                        if (dsfood[i].RestaurantID != dsfood[j].RestaurantID)
//                        {
//                            listOfFoods.Add(dsfood[i]);
//                        }
//                    }
//                }
//            }

//            foreach (var item in listOfFoods)
//            {
//                listRestaurants.Add(await getRestaurantByIdType(item.RestaurantID));
//            }

//            return View("ListOfRestaurant", listRestaurants);
//        }
//    }
//}
//else
//{
//    List<RestaurantsModel> listRestaurants = new List<RestaurantsModel>();

//    string[] detailDistricts = listDistricts;

//    foreach(string item in detailDistricts)
//    {
//        int IdDistricts = int.Parse(item);

//        using (var httpClient = new HttpClient())
//        {
//            configHttpClient(httpClient);

//            HttpResponseMessage respone = await httpClient.GetAsync("api/restaurant/getListOfrestaurantByIdDistricts/" + IdDistricts +"");

//            if(respone.IsSuccessStatusCode)
//            {
//                var restaurantTask = respone.Content.ReadAsAsync<List<RestaurantsModel>>();

//                restaurantTask.Wait();

//                var listRests = restaurantTask.Result;

//                for(int i = 0; i<listRests.Count; i++)
//                {
//                    listRestaurants.Add(listRests[i]);
//                }
//            }
//        }
//    }
//    return View("ListOfRestaurant", listRestaurants);
//}
//return BadRequest();