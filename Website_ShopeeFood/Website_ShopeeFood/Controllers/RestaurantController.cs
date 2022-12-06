﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System;
using Website_ShopeeFood.Models;
using Newtonsoft.Json;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Text;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using static System.Collections.Specialized.BitVector32;
using Website_ShopeeFood.Services;
using System.Net.WebSockets;

namespace Website_ShopeeFood.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly IAPIServices aPIServices;

                    //Return A Food filtered By Id
        public static List<FoodModel> listFood = new List<FoodModel>();

         static int tongTien = 0;

        public RestaurantController(IAPIServices aPIServices)
        {
            this.aPIServices = aPIServices;
        }

        List<RestaurantsModel> ListofRestaurant = new List<RestaurantsModel>();

        public static string[] listDistricts;

        public static string[] listTypes;

        public static int soLanThemQuan = 6;

        //Trả về partial view chứa các phương thức load danh sách quán ăn và lọc theo khu vực với phân loại
        [HttpGet]
        public async Task<IActionResult> Restaurant_Partial()
        {

            if (HttpContext.Session.GetString("CheckGetType").IsNullOrEmpty())
            {
                if (HttpContext.Session.GetString("AreaIDofRestaurant") == null)
                {
                    HttpContext.Session.SetString("AreaIDofRestaurant", "1");
                }

                List<RestaurantsModel> restaurant = new List<RestaurantsModel>();

                restaurant = await aPIServices.GetAllRestaurant();

                List<RestaurantsModel> listRes = new List<RestaurantsModel>();

                for (int i = 0; i < soLanThemQuan; i++)
                {
                    if (restaurant[i].AreaID == int.Parse(HttpContext.Session.GetString("AreaIDofRestaurant")))
                    {
                        listRes.Add(restaurant[i]);
                    }
                }

                return PartialView("Restaurant_Partial", listRes);

            }
            else
            {
                if (bool.Parse(HttpContext.Session.GetString("CheckGetType")) == false)
                {
                    if (HttpContext.Session.GetString("AreaIDofRestaurant") == null)
                    {
                        HttpContext.Session.SetString("AreaIDofRestaurant", "1");
                    }

                    List<RestaurantsModel> restaurant = new List<RestaurantsModel>();

                    restaurant = await aPIServices.GetAllRestaurant();

                    List<RestaurantsModel> listRes = new List<RestaurantsModel>();

                    for (int i = 0; i < soLanThemQuan; i++)
                    {
                        if (restaurant[i].AreaID == int.Parse(HttpContext.Session.GetString("AreaIDofRestaurant")))
                        {
                            listRes.Add(restaurant[i]);
                        }
                    }

                    return PartialView("Restaurant_Partial", listRes);

                }
                else
                {
                    if (HttpContext.Session.GetString("AreaIDofRestaurant") == null)
                    {
                        HttpContext.Session.SetString("AreaIDofRestaurant", "1");
                    }

                    List<RestaurantsModel> restaurant_type = new List<RestaurantsModel>();

                    restaurant_type = await aPIServices.GetAllRestaurant();

                    List<RestaurantsModel> listRes = new List<RestaurantsModel>();

                    for (int i = 0; i < soLanThemQuan; i++)
                    {
                        if (restaurant_type[i].AreaID == int.Parse(HttpContext.Session.GetString("AreaIDofRestaurant")))
                        {
                            listRes.Add(restaurant_type[i]);
                        }
                    }

                    return PartialView("Restaurant_Partial", listRes);
                }
            }
            return NotFound();
        }

        //Pressing Loadmore Button Then Adding Three New Of Restaurant
        [HttpGet]
        public IActionResult addNumberOfRestaurant()
        {
            soLanThemQuan = soLanThemQuan + 3;
            return RedirectToAction("Index", "Home");
        }



        [HttpGet]
        public IActionResult Search_Restaurant()
        {
            return PartialView("Search_Restaurant");
        }

        //The Action When Search Name Of Restaurant
        [HttpGet("searchListOfRestaurant/{name}")]
        public async Task<IActionResult> searchListOfRestaurant(string name)
        {
            List<RestaurantsModel> listRes = new List<RestaurantsModel>();

            List<RestaurantsModel> listSearchedRes = new List<RestaurantsModel>();

            listRes = await aPIServices.GetAllRestaurant();

            listSearchedRes = await aPIServices.searchListRestaurantByName(name, listRes);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> listOfRestaurant()
        {
            int check = int.Parse(HttpContext.Session.GetString("CheckingList"));
            if (check == 0)
            {
                List<RestaurantsModel> restaurant = new List<RestaurantsModel>();

                restaurant = await aPIServices.GetAllRestaurant();

                List<RestaurantsModel> listRes = new List<RestaurantsModel>();

                foreach (var item in restaurant)
                {
                    if (item.AreaID == int.Parse(HttpContext.Session.GetString("AreaIDofRestaurant")))
                    {
                        listRes.Add(item);
                    }
                }

                return PartialView("listOfRestaurant", listRes);
            }
            else if (check == 1)
            {
                List<FoodModel> listOfFoods = new List<FoodModel>();

                List<RestaurantsModel> listRestaurants = new List<RestaurantsModel>();

                int Id = int.Parse(HttpContext.Session.GetString("IdTypeOfFood"));


                List<FoodModel> dsfood = await aPIServices.getListRestaurantBasedOnTypeID(Id);

                if (dsfood.Count != 0)
                {
                    listOfFoods.Add(dsfood[0]);

                    for (int i = 1; i < dsfood.Count - 1; i++)
                    {
                        if (dsfood[i].RestaurantID != dsfood[i++].RestaurantID)
                        {
                            listOfFoods.Add(dsfood[i]);
                        }
                    }
                }

                foreach (var item in listOfFoods)
                {
                    listRestaurants.Add(await aPIServices.getRestaurantsByIdRestaurant(item.RestaurantID));
                }

                return PartialView("listOfRestaurant", listRestaurants);

            }
            else if (check == 2)
            {
                List<RestaurantsModel> listRestaurants = new List<RestaurantsModel>();

                string[] detailDistricts = listDistricts;

                foreach (string item in detailDistricts)
                {
                    int IdDistricts = int.Parse(item);

                    List<RestaurantsModel> listRests = await aPIServices.getListRestauranrByIdDistricts(IdDistricts);

                    for (int i = 0; i < listRests.Count; i++)
                    {
                        listRestaurants.Add(listRests[i]);
                    }

                }
                return PartialView("listOfRestaurant", listRestaurants);
            }
            else
            {
                List<FoodModel> listOfFoods = new List<FoodModel>();

                List<RestaurantsModel> listRestaurantsNotFiltered = new List<RestaurantsModel>();

                List<RestaurantsModel> listRestaurantsFiltered = new List<RestaurantsModel>();

                string[] DetailTypes = listTypes;

                foreach (string item in DetailTypes)
                {
                    int IdTypes = int.Parse(item);

                    using (var httpClient = new HttpClient())
                    {
                        configHttpClient(httpClient);

                        HttpResponseMessage respone = await httpClient.GetAsync("api/food/getListRestaurantBasedOnTypeId/" + IdTypes + "");

                        if (respone.IsSuccessStatusCode)
                        {
                            var restaurantTask = respone.Content.ReadAsAsync<List<FoodModel>>();

                            restaurantTask.Wait();

                            var listRests = restaurantTask.Result;

                            if (listRests.Count != 0)
                            {
                                listOfFoods.Add(listRests[0]);

                                for (int i = 1; i < listRests.Count - 1; i++)
                                {
                                    if (listRests[i].RestaurantID != listRests[i++].RestaurantID)
                                    {
                                        listOfFoods.Add(listRests[i]);
                                    }
                                }
                            }

                            foreach (var listItem in listOfFoods)
                            {
                                listRestaurantsNotFiltered.Add(await aPIServices.getRestaurantsByIdRestaurant(listItem.RestaurantID));
                            }
                        }
                    }
                }

                if (listRestaurantsNotFiltered.Count != 0)
                {
                    int k = 0;

                    listRestaurantsFiltered.Add(listRestaurantsNotFiltered[k]);

                    for (int i = 1; i < listRestaurantsNotFiltered.Count; i++)
                    {
                        if (listRestaurantsFiltered[k].RestaurantID != listRestaurantsNotFiltered[i].RestaurantID)
                        {
                            listRestaurantsFiltered.Add(listRestaurantsNotFiltered[i]);
                            k++;
                        }
                    }
                }

                return PartialView("listOfRestaurant", listRestaurantsFiltered);
            }
            return BadRequest();
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
            listDistricts = list_districts;
            if (listDistricts.IsNullOrEmpty())
            {
                HttpContext.Session.SetString("CheckingList", "0");
            }
            else
            {
                HttpContext.Session.SetString("CheckingList", "2");
            }

            return RedirectToAction("ListOfRestaurant", "Home");
        }

        [HttpPost]
        public IActionResult filterRestaurantByTypes(string[] list_types)
        {
            listTypes = list_types;
            if (listTypes.IsNullOrEmpty())
            {
                HttpContext.Session.SetString("CheckingList", "0");
            }
            else
            {
                HttpContext.Session.SetString("CheckingList", "3");
            }

            return RedirectToAction("ListOfRestaurant", "Home");
        }


        //Get The Restaurants By Options
        [HttpGet]
        public async Task<IActionResult> Brand_Partial()
        {
            List<RestaurantsModel> restaurant = new List<RestaurantsModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(aPIServices.getIPAddress());

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage message = await client.GetAsync("api/restaurant");

                if (message.IsSuccessStatusCode)
                {
                    var restaurantMessage = message.Content.ReadAsStringAsync().Result;

                    restaurant = JsonConvert.DeserializeObject<List<RestaurantsModel>>(restaurantMessage);
                }
            }

            List<FoodModel> foods = new List<FoodModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(aPIServices.getIPAddress());

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage message = await client.GetAsync("api/food");

                if (message.IsSuccessStatusCode)
                {
                    var foodsinRest = message.Content.ReadAsStringAsync().Result;

                    foods = JsonConvert.DeserializeObject<List<FoodModel>>(foodsinRest);
                }
            }
            var brand = new List<BrandModel>();

            for (int i = 0; i < restaurant.Count; i++)
            {
                brand[i].nameRestaurant = restaurant[i].NameofRestaurant;
                brand[i].numberofRestaurant = 1;
                brand[i].address = restaurant[i].Address;
                brand[i].image = restaurant[i].Image;
                brand[i].lowestPrices = 0;
                brand[i].prices = 0;
            }

            for (int i = 0; i < foods.Count; i++)
            {
                for (int j = 1; j < foods.Count - 1; j++)
                {
                    if (foods[i].Price > foods[j].Price && restaurant[i].ID == restaurant[j].ID)
                        brand[i].lowestPrices = foods[j].Price;
                }

                for (int j = 1; j < foods.Count - 1; j++)
                {
                    if (foods[i].Price < foods[j].Price && restaurant[i].ID == restaurant[j].ID)
                        brand[i].prices = foods[j].Price;
                }
            }

            if (brand != null)
            {
                return PartialView("Brand_Partial", brand);
            }
            else
            {
                return NotFound();
            }
        }

        //Lấy ID Khu Vực Trả Về List Quán Ăn
        [HttpGet]
        public async Task<IActionResult> AreaID(int AreaID)
        {

            var model = await aPIServices.GetAllRestaurant();
            List<RestaurantsModel> u = new List<RestaurantsModel>();

            foreach (var item in model)
            {
                if (item.AreaID == AreaID)
                {
                    u.Add(item);
                }
            }

            using (var httpClient = new HttpClient())
            {
                string baseURL = aPIServices.getIPAddress();

                httpClient.BaseAddress = new Uri(baseURL);

                httpClient.DefaultRequestHeaders.Clear();

                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await httpClient.GetAsync("api/restaurant/GetRestaurantByID?AreaID=" + AreaID + "");

                if (response.IsSuccessStatusCode)
                {
                    var restaurantTask = response.Content.ReadAsAsync<List<RestaurantsModel>>();

                    restaurantTask.Wait();

                    var dsrestaurant = restaurantTask.Result;

                    foreach (var item in dsrestaurant)
                    {
                        ListofRestaurant.Add(item);
                    }

                    HttpContext.Session.SetString("AreaIDofRestaurant", AreaID.ToString());

                    HttpContext.Session.SetString("CheckGetType", "false");

                    ViewBag.ID = AreaID.ToString();

                    return RedirectToAction("Index", "Home");
                }

                return BadRequest();
            }
        }

        //Lấy Get ID Loại Món Ăn Trả Về Quán Ăn

        [HttpGet]
        public async Task<IActionResult> TypeID(int ID)
        {

            var model = await aPIServices.GetAllRestaurant();
            List<RestaurantsModel> u = new List<RestaurantsModel>();

            foreach (var item in model)
            {
                if (item.ID == ID)
                {
                    u.Add(item);
                }
            }

            using (var httpClient = new HttpClient())
            {
                string baseURL = aPIServices.getIPAddress();

                httpClient.BaseAddress = new Uri(baseURL);

                httpClient.DefaultRequestHeaders.Clear();

                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await httpClient.GetAsync("api/restaurant/GetRestaurantByTypeID?ID=" + ID + "");

                if (response.IsSuccessStatusCode)
                {
                    var restaurantTask = response.Content.ReadAsAsync<List<RestaurantsModel>>();

                    restaurantTask.Wait();

                    var dsrestaurant = restaurantTask.Result;

                    foreach (var item in dsrestaurant)
                    {
                        ListofRestaurant.Add(item);
                    }

                    HttpContext.Session.SetString("TypeIDofRestaurant", ID.ToString());

                    HttpContext.Session.SetString("CheckGetType", "true");

                    return RedirectToAction("Index", "Home");
                }

                return BadRequest();
            }
        }


        //Lấy thông tin chi tiết quán ăn
        //api /Restaurant/GetrestaurantByID lấy ID quán để lấy thông tin chi tiết
        [HttpGet]
        public async Task<IActionResult> GetRestaurantByID(int restaurantId)
        {
            RestaurantsModel res = new RestaurantsModel();

            restaurantId = int.Parse(HttpContext.Session.GetString("restaurantId"));

            using (var httpclient = new HttpClient())
            {
                string baseURL = aPIServices.getIPAddress();

                httpclient.BaseAddress = new Uri(baseURL);

                httpclient.DefaultRequestHeaders.Clear();

                httpclient.DefaultRequestHeaders.Accept?.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage responseMessage = await httpclient.GetAsync("api/restaurant/GetIDOfRestaurant/" + restaurantId + "");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var Restaurant = responseMessage.Content.ReadAsAsync<RestaurantsModel>();

                    Restaurant.Wait();

                    var DetailRestaurant = Restaurant.Result;

                    res = DetailRestaurant;

                    return PartialView("GetRestaurantByID", res);
                }
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> DetailOfRestaurant(int restaurantId, int areaId)
        {
            if (HttpContext.Session.GetString("ImageUser") != null)
            {
                ViewBag.ImgaeUser = HttpContext.Session.GetString("ImageUser");
            }
            else
            {
                ViewBag.ImgaeUser = null;
            }

            if (HttpContext.Session.GetString("FullName") != null)
            {
                ViewBag.NameUser = HttpContext.Session.GetString("FullName");
            }
            else
            {
                ViewBag.NameUser = null;
            }

            //Get IdFood
            if (HttpContext.Session.GetString("foodId") != null)
            {
                int foodId = int.Parse(HttpContext.Session.GetString("foodId"));

                FoodModel food = new FoodModel();

                food = await aPIServices.getFoodById(foodId);

                listFood.Add(food);


                tongTien = tongTien + int.Parse(food.Price.ToString()); 


                ViewData["TotalPrice"] = tongTien;

                return View("DetailOfRestaurant", listFood);
            }
            else
            {
                listFood.Clear();
                tongTien = 0;
                HttpContext.Session.SetString("restaurantId", restaurantId.ToString());
                HttpContext.Session.SetString("AreaID", areaId.ToString());
                ViewData["TotalPrice"] = 12;
                return View();
            }

            
        }



        [HttpGet]
        public async Task<IActionResult> getFoodByID(int foodId)
        {

            HttpContext.Session.SetString("foodId", foodId + "");
            //if (foodId == 0)
            //{
            //    foodId = 1;
            //}

            //if (HttpContext.Session.GetString("FullName") != null)
            //{
            //    FoodModel food = new FoodModel();

            //    food = await aPIServices.getFoodById(foodId);

            //    listFood.Add(food);

            //    tongTien = tongTien + food.Price;



            //    HttpContext.Session.SetString("Total", tongTien + "");

            //    return RedirectToAction("DetailOfRestaurant");
            //}
            //else
            //{
            //    ViewData["AddToCart"] = "Not Login Already!";
            //    return RedirectToAction("DetailOfRestaurant");
            //}
            //return RedirectToAction("DetailOfRestaurant", "Restaurant");
            return RedirectToAction("DetailOfRestaurant");
        }


        //Lấy ID Khu Vực Trả Về List Quán Ăn
        [HttpGet]
        public async Task<IActionResult> listOfRestaurantBasedOnTypeOfFood(int ID)
        {
            List<FoodModel> listOfFoods = new List<FoodModel>();

            List<RestaurantsModel> listRestaurants = new List<RestaurantsModel>();

            using (var httpClient = new HttpClient())
            {
                configHttpClient(httpClient);

                HttpResponseMessage response = await httpClient.GetAsync("api/food/getListRestaurantBasedOnTypeId/" + ID + "");

                if (response.IsSuccessStatusCode)
                {
                    var foodsTask = response.Content.ReadAsAsync<List<FoodModel>>();

                    foodsTask.Wait();

                    var dsfood = foodsTask.Result;

                    if (dsfood != null)
                    {
                        listOfFoods.Add(dsfood[0]);
                    }

                    for (int i = 1; i < dsfood.Count; i++)
                    {
                        for (int j = i + 1; j < dsfood.Count - 1; j++)
                        {
                            if (dsfood[i].RestaurantID != dsfood[j].RestaurantID)
                            {
                                listOfFoods.Add(dsfood[i]);
                            }
                        }
                    }

                    foreach (var item in listOfFoods)
                    {
                        listRestaurants.Add(await aPIServices.getRestaurantsByIdRestaurant(item.RestaurantID));
                    }

                    ViewData["restaurant"] = listRestaurants;

                    return RedirectToAction("ListOfRestaurant", "Home", new { restaurantsModels = listOfFoods });
                }

                return BadRequest();
            }
        }

        public void configHttpClient(HttpClient httpClient)
        {
            string baseURL = aPIServices.getIPAddress();

            httpClient.BaseAddress = new Uri(baseURL);

            httpClient.DefaultRequestHeaders.Clear();

            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
