using Microsoft.AspNetCore.Mvc;
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
using Microsoft.Extensions.Logging;

namespace Website_ShopeeFood.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly IAPIServices aPIServices;

        //Return A Food filtered By Id
        public static List<FoodModel> listFood = new List<FoodModel>();

        public static List<ItemsModel> listCart = new List<ItemsModel>();

        static int tongTien = 0;

        public RestaurantController(IAPIServices aPIServices)
        {
            this.aPIServices = aPIServices;
        }

        List<RestaurantsModel> ListofRestaurant = new List<RestaurantsModel>();

        public static string[] listDistricts;

        public static string[] listTypes;

        public static int soLanThemQuan = 9;

        public static int brandRestaurant = 9;

        public static bool checkingNumberofRestaurant = false;


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

                if (checkingNumberofRestaurant == true)
                {
                    int soLan = 9;

                    for (int i = 0; i < soLan; i++)
                    {
                        if (restaurant[i].AreaID == int.Parse(HttpContext.Session.GetString("AreaIDofRestaurant")))
                        {
                            listRes.Add(restaurant[i]);
                        }
                        else
                        {
                            if(i >= soLanThemQuan-1)
                            {
                                soLan++;
                            }
                        }
                    }
                    return PartialView("Restaurant_Partial", listRes);
                }
                else
                {
                    for (int i = 0; i < soLanThemQuan; i++)
                    {
                        if (restaurant[i].AreaID == int.Parse(HttpContext.Session.GetString("AreaIDofRestaurant")))
                        {
                            listRes.Add(restaurant[i]);
                        }
                    }
                    return PartialView("Restaurant_Partial", listRes);
                }  
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

                    int soLan = 9;

                    for (int i = 0; i < soLan; i++)
                    {
                        if (restaurant[i].AreaID == int.Parse(HttpContext.Session.GetString("AreaIDofRestaurant")))
                        {
                            listRes.Add(restaurant[i]);
                        }
                        else
                        {
                            if (i >= soLanThemQuan - 1)
                            {
                                soLan++;
                            }
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

                    int soLan = 9;

                    for (int i = 0; i < soLan; i++)
                    {
                        if ((restaurant_type[i].ID == int.Parse(HttpContext.Session.GetString("TypeIDofRestaurant")) && (restaurant_type[i].AreaID == int.Parse(HttpContext.Session.GetString("AreaIDofRestaurant")))))
                        {
                            listRes.Add(restaurant_type[i]);
                        }
                        else
                        {
                            if (i >= soLanThemQuan - 1)
                            {
                                soLan++;
                            }
                        }
                    }

                    return PartialView("Restaurant_Partial", listRes);
                }
            }
            return NotFound();
        }

        public IActionResult filteredListOfRestaurantByID(int areaID)
        {
            HttpContext.Session.SetString("AreaIDofRestaurant", areaID.ToString());
            checkingNumberofRestaurant = true;
            return RedirectToAction("Index", "Home");
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
            return PartialView("Search_Restaurant", listSearchedRes);
        }

        public static List<RestaurantsModel> listSearchedRes = new List<RestaurantsModel>();

        //The Action When Search Name Of Restaurant
        public async Task<IActionResult> searchListOfRestaurant(string name)
        {
            if (name != null)
            {
                List<RestaurantsModel> listRes = new List<RestaurantsModel>();

                listRes = await aPIServices.GetAllRestaurant();

                listSearchedRes = await aPIServices.searchListRestaurantByName(name, listRes);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                listSearchedRes.Clear();

                return RedirectToAction("Index", "Home");
            }
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

        public async Task<IActionResult> returnListOfRestaurant()
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
        public IActionResult returnListRestaurantByDistricts(int result)
        {

            if (result == 0)
            {
                HttpContext.Session.SetString("CheckingList", "0");
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

            restaurant = await aPIServices.GetAllRestaurant();

            List<BrandModel> brandModels = new List<BrandModel>();

            int soLuong = 0;

            foreach(var item in restaurant)
            {
                if (soLuong < brandRestaurant)
                {
                    brandModels.Add(new BrandModel { Restaurants = item });
                    soLuong++;
                }
            }

            if (brandModels != null)
            {
                return PartialView("Brand_Partial", brandModels);
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

        static bool check = true;

        [HttpGet]
        public async Task<IActionResult> DetailOfRestaurant(int restaurantId, int areaId)   
        {
            if (restaurantId != 0)
            {
                HttpContext.Session.SetString("restaurantIdWhenClick", restaurantId.ToString());
            }

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

            if(HttpContext.Session.GetString("UserIdToCheckInvoices") != null)
            {
                int userID = int.Parse(HttpContext.Session.GetString("UserIdToCheckInvoices"));

                List<AddressUserModel> addressUserModel = new List<AddressUserModel>();

                addressUserModel = await aPIServices.getListAddressUserByUserId(userID);

                ViewBag.ListAddressUser = addressUserModel;
            }
            else
            {
                ViewBag.ListAddressUser = null;
            }

            if (HttpContext.Session.GetString("FullName") != null)
            {
                //Get IdFood
                if (HttpContext.Session.GetString("foodId") != null && HttpContext.Session.GetString("restaurantId") == HttpContext.Session.GetString("restaurantIdWhenClick"))
                {
                    if (check == true)
                    {

                        if (listCart.Count == 0)
                        {
                            List<ItemsModel> cart = new List<ItemsModel>();

                            int foodId = int.Parse(HttpContext.Session.GetString("foodId"));

                            FoodModel food = new FoodModel();

                            food = await aPIServices.getFoodById(foodId);

                            listFood.Add(food);

                            cart.Add(new ItemsModel { Food = food, Quantity = 1 });

                            listCart = cart;
                        }
                        else
                        {
                            List<ItemsModel> cart = listCart;

                            int foodId = int.Parse(HttpContext.Session.GetString("foodId"));

                            int j = 0;

                            for (int i = 0; i < cart.Count; i++)
                            {
                                if (cart[i].Food.FoodId.Equals(foodId))
                                {
                                    j = i;
                                    break;
                                }
                                else
                                {
                                    j = -1;
                                }
                            }

                            FoodModel food = new FoodModel();

                            food = await aPIServices.getFoodById(foodId);

                            if (j != -1)
                            {
                                cart[j].Quantity++;
                            }else
                            {
                                cart.Add(new ItemsModel { Food = food, Quantity = 1 });
                            }

                            listCart = cart;
                        }

                        check = false;
                    }

                    return View("DetailOfRestaurant", listCart);
                }
                else
                {
                    listFood.Clear();
                    listCart.Clear();
                    tongTien = 0;
                    HttpContext.Session.SetString("restaurantId", restaurantId.ToString());
                    HttpContext.Session.SetString("AreaID", areaId.ToString());
                    ViewData["TotalPrice"] = 12;
                    return View();
                }
            }
            else
            {
                if (HttpContext.Session.GetString("restaurantId") != HttpContext.Session.GetString("restaurantIdWhenClick") && int.Parse(HttpContext.Session.GetString("restaurantIdWhenClick")) != 0)
                {
                    HttpContext.Session.SetString("restaurantId", restaurantId.ToString());
                    HttpContext.Session.SetString("AreaID", areaId.ToString());
                }
                TempData["checkAccountLogin"] = "You Need To Login First!";

                ViewBag.CheckAccountAddToCart = TempData["checkAccountLogin"] as string;
                return View();
            }
        }


        public IActionResult resetTheMenu()
        {
            listCart.Clear();
            listFood.Clear();
            return RedirectToAction("DetailOfRestaurant");
        }

        public  IActionResult reduceNumberOfIdFood(int foodId)
        {
            for (int i = 0; i < listCart.Count; i++)
            {
                if (listCart[i].Food.FoodId.Equals(foodId))
                {
                    if (listCart[i].Quantity > 1)
                    {
                        listCart[i].Quantity--;
                    }
                    else
                    {
                        listCart.RemoveAt(i);
                    }
                    break;
                }
            }
            return RedirectToAction("DetailOfRestaurant", listCart);
        }

        public IActionResult addNumberOfIdFood(int foodId)
        {
            for (int i = 0; i < listCart.Count; i++)
            {
                if (listCart[i].Food.FoodId.Equals(foodId))
                {
                    listCart[i].Quantity++;
                    break;
                }
            }

            return View("DetailOfRestaurant", listCart);
        }


        [HttpGet]
        public IActionResult getFoodByID(int foodId)
        {
            if (foodId != 0)
            {
                check = true;
                HttpContext.Session.SetString("foodId", foodId + "");
            }
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
