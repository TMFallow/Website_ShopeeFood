using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NuGet.Packaging.Signing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Website_ShopeeFood.Models;

namespace Website_ShopeeFood.Services
{
    public class APIServices : IAPIServices
    {
        private readonly IConfiguration configuration;

        public APIServices(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string getIPAddress()
        {
            string IPAddress = configuration["Website_ShopeeFood_API:Base_URL"];

            return IPAddress;
        }

        public string getIPAddressMVC()
        {
            string IpMVC = "";
            return IpMVC;
        }


        public async Task<List<AreasModel>> getArea()
        {
            List<AreasModel> areas = new List<AreasModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(getIPAddress()); //Chuyển URL

                client.DefaultRequestHeaders.Clear();

                //Định dạng format dữ liệu là JSon

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                // Gửi yêu cầu tìm dịch vụ Web API bằng cách sử dụng HttpClient

                HttpResponseMessage message = await client.GetAsync("api/areas");

                //Kiểm tra xem có thành không ko

                if (message.IsSuccessStatusCode)
                {
                    //Lưu trữ phản hồi sau ghi gọi api
                    var areaMessage = message.Content.ReadAsStringAsync().Result;

                    areas = JsonConvert.DeserializeObject<List<AreasModel>>(areaMessage);
                }
            }

            if (areas != null)
            {
                return areas;
            }
            else
            {
                return null;
            }
        }

        public async Task<AreasModel> getNameOfArea(int AreaId)
        {
            AreasModel areas = new AreasModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(getIPAddress());

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage message = await client.GetAsync("areas/getNameAreas/" + AreaId + "");

                if (message.IsSuccessStatusCode)
                {
                    var areaMessage = message.Content.ReadAsStringAsync().Result;

                    areas = JsonConvert.DeserializeObject<AreasModel>(areaMessage);
                }
            }

            if (areas != null)
            {
                return areas;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<DetailAreasModel>> getDetailAreas(int AreaId)
        {
            List<DetailAreasModel> model = new List<DetailAreasModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(getIPAddress());

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage message = await client.GetAsync("api/Detailareas/getDetailAreasByID/" + AreaId + "");

                if (message.IsSuccessStatusCode)
                {
                    var detailAreaMessage = message.Content.ReadAsAsync<IEnumerable<DetailAreasModel>>();

                    detailAreaMessage.Wait();

                    var listDistrict = detailAreaMessage.Result;

                    foreach (var item in listDistrict)
                    {
                        model.Add(item);
                    }
                }
            }

            if (model != null)
            {
                return model;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<FoodModel>> getAllFoodByIdRestaurant(int restaurantId)
        {
            List<FoodModel> foods = new List<FoodModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(getIPAddress());

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage message = await client.GetAsync("api/food/getAllRestaurantUsingIDRestaurant/" + restaurantId + "");

                if (message.IsSuccessStatusCode)
                {
                    var foodMessage = message.Content.ReadAsAsync<IEnumerable<FoodModel>>();

                    foodMessage.Wait();

                    var dsFood = foodMessage.Result;

                    foreach (var item in dsFood)
                    {
                        foods.Add(item);
                    }
                }
            }

            if (foods != null)
            {
                return foods;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<UsersModel>> GetListUsers()
        {
            List<UsersModel> users = new List<UsersModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(getIPAddress());

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage message = await client.GetAsync("api/users/GetUsers");

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

        public async Task<UsersModel> GetUsersByEmail(string email)
        {
            UsersModel model = new UsersModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(getIPAddress());

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage message = await client.GetAsync("api/users/getUserByEmail/" + email + "");

                if (message.IsSuccessStatusCode)
                {
                    var userTask = message.Content.ReadAsAsync<UsersModel>();

                    userTask.Wait();

                    model = userTask.Result;
                }
            }
            return model;
        }

        public async Task<UsersModel> getUsersByUsername(string username)
        {
            UsersModel usersModel = new UsersModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(getIPAddress());

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage message = await client.GetAsync("api/users/getUserByUsername/" + username + "");

                if (message.IsSuccessStatusCode)
                {
                    var userTask = message.Content.ReadAsAsync<UsersModel>();

                    userTask.Wait();

                    usersModel = userTask.Result;
                }
            }

            if (usersModel != null)
            {
                return usersModel;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<PromotionModels>> getPromotion()
        {
            List<PromotionModels> promotions = new List<PromotionModels>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(getIPAddress());

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage message = await client.GetAsync("api/promotion");

                if (message.IsSuccessStatusCode)
                {
                    var promotionsMessage = message.Content.ReadAsStringAsync().Result;

                    var dsKhuyenMai = JsonConvert.DeserializeObject<List<PromotionModels>>(promotionsMessage);

                    for (int i = 1; i < dsKhuyenMai.Count; i++)
                    {
                        promotions.Add(dsKhuyenMai[i]);
                    }
                }
            }

            if (promotions != null)
            {
                return promotions;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<RestaurantsModel>> GetAllRestaurant()
        {
            List<RestaurantsModel> restaurant = new List<RestaurantsModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(getIPAddress());

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage message = await client.GetAsync("api/restaurant/GetRestaurant");

                if (message.IsSuccessStatusCode)
                {
                    var restaurantMessage = message.Content.ReadAsStringAsync().Result;

                    var dsrestaurant = JsonConvert.DeserializeObject<List<RestaurantsModel>>(restaurantMessage);

                    foreach (var item in dsrestaurant)
                    {
                        restaurant.Add(item);
                    }

                    return restaurant;
                }
                return null;
            }
        }

        public async Task<RestaurantsModel> getRestaurantsByIdRestaurant(int restaurantId)
        {
            RestaurantsModel restaurants = new RestaurantsModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(getIPAddress());

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/restaurant/GetListRestaurant/" + restaurantId + "");

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

        public async Task<List<RestaurantsModel>> searchListRestaurantByName(string name, List<RestaurantsModel> restaurantsModels)
        {
            List<RestaurantsModel> restaurant = new List<RestaurantsModel>();

            restaurant = restaurantsModels.Where(nameRes => nameRes.NameofRestaurant.ToUpper().Contains(name.ToUpper())).ToList();

            return restaurant;
        }

        public async Task<List<FoodModel>> searchListRestaurantByTypeID(int IdTypes)
        {
            List<FoodModel> listRests = new List<FoodModel>();

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(getIPAddress());

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage respone = await client.GetAsync("api/food/getListRestaurantBasedOnTypeId/" + IdTypes + "");

                if (respone.IsSuccessStatusCode)
                {
                    var restaurantTask = respone.Content.ReadAsAsync<List<FoodModel>>();

                    restaurantTask.Wait();

                    listRests = restaurantTask.Result;
                }
            }
            return listRests;
        }

        public async Task<FoodModel> getFoodById(int Id)
        {

            FoodModel food = new FoodModel();

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(getIPAddress());

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage respone = await client.GetAsync("api/food/getFoodById/" + Id + "");

                if (respone.IsSuccessStatusCode)
                {
                    var foodTask = respone.Content.ReadAsAsync<FoodModel>();

                    foodTask.Wait();

                    food = foodTask.Result;
                }
            }
            return food;
        }

        public async Task<List<FoodModel>> getListRestaurantBasedOnTypeID(int Id)
        {
            List<FoodModel> listRestaurants = new List<FoodModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(getIPAddress());

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/food/getListRestaurantBasedOnTypeId/" + Id + "");

                if (response.IsSuccessStatusCode)
                {
                    var foodsTask = response.Content.ReadAsAsync<List<FoodModel>>();

                    foodsTask.Wait();

                    listRestaurants = foodsTask.Result;
                }

                return listRestaurants;
            }
        }

        public async Task<List<RestaurantsModel>> getListRestauranrByIdDistricts(int IdDistricts)
        {
            List<RestaurantsModel> listRestaurant = new List<RestaurantsModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(getIPAddress());

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage respone = await client.GetAsync("api/restaurant/getListOfrestaurantByIdDistricts/" + IdDistricts + "");

                if (respone.IsSuccessStatusCode)
                {
                    var restaurantTask = respone.Content.ReadAsAsync<List<RestaurantsModel>>();

                    restaurantTask.Wait();

                    listRestaurant = restaurantTask.Result;
                }
            }
            return listRestaurant;
        }

        public async Task<List<InvoicesModel>> getListInvoicesByUserID(int userId)
        {
            List<InvoicesModel> listInvoiceByUserId = new List<InvoicesModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(getIPAddress()); 

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage respone = await client.GetAsync("api/Invoice/getListOfInvoicesByUserID/" + userId + "");

                if (respone.IsSuccessStatusCode)
                {
                    var invoicesTask = respone.Content.ReadAsAsync<List<InvoicesModel>>();

                    invoicesTask.Wait();

                    listInvoiceByUserId = invoicesTask.Result;
                }
            }
            return listInvoiceByUserId;
        }

        public async void insertInvoices(InvoicesModel invoicesModel)
        {
            InvoicesModel invoices = new InvoicesModel();

            if (invoicesModel != null)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(getIPAddress());

                    client.DefaultRequestHeaders.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    StringContent content = new StringContent(JsonConvert.SerializeObject(invoicesModel),
                    Encoding.UTF8, "application/json");

                    HttpResponseMessage respone = await client.PostAsync("api/Invoice/insertInvoice", content);

                }
            }
        }

        public async void insertInvoiceDetails(InvoiceDetailsModel invoiceDetailsModel)
        {
            if (invoiceDetailsModel != null)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(getIPAddress());

                    client.DefaultRequestHeaders.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    StringContent content = new StringContent(JsonConvert.SerializeObject(invoiceDetailsModel),
                    Encoding.UTF8, "application/json");

                    HttpResponseMessage respone = await client.PostAsync("api/InvoiceDetails/insertInvoicesDetails", content);
                }
            }
        }

        public async Task<List<InvoicesModel>> getAllInvoices()
        {
            List<InvoicesModel> listInvoiceByUserId = new List<InvoicesModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(getIPAddress());

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage respone = await client.GetAsync("api/Invoice/getAllInvoice");

                if (respone.IsSuccessStatusCode)
                {
                    var invoicesTask = respone.Content.ReadAsAsync<List<InvoicesModel>>();

                    invoicesTask.Wait();

                    listInvoiceByUserId = invoicesTask.Result;
                }
            }
            return listInvoiceByUserId;
        }

        public async Task<List<InvoiceDetailsModel>> getListDetailsInvoiceByInvoices(int invoicesId)
        {
            List<InvoiceDetailsModel> listDetailInvoiceModel = new List<InvoiceDetailsModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(getIPAddress());

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage respone = await client.GetAsync("api/InvoiceDetails/getListOfInvoicesDetailByIdInvoices/"+ invoicesId +"");

                if (respone.IsSuccessStatusCode)
                {
                    var detailInvoicesTask = respone.Content.ReadAsAsync<List<InvoiceDetailsModel>>();

                    detailInvoicesTask.Wait();

                    listDetailInvoiceModel = detailInvoicesTask.Result;
                }
            }
            return listDetailInvoiceModel;
        }

        public async Task<UsersModel> getUserById(int Id)
        {
            UsersModel userModel = new UsersModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(getIPAddress());

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage respone = await client.GetAsync("api/Users/GetUserById/" + Id + "");

                if (respone.IsSuccessStatusCode)
                {
                    var usersTask = respone.Content.ReadAsAsync<UsersModel>();

                    usersTask.Wait();

                    userModel = usersTask.Result;
                }
            }
            return userModel;
        }

        public async void updateUsersInfo(UsersModel usersModel)
        {
            if (usersModel != null)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(getIPAddress());

                    client.DefaultRequestHeaders.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    StringContent content = new StringContent(JsonConvert.SerializeObject(usersModel),
                    Encoding.UTF8, "application/json");

                    HttpResponseMessage respone = await client.PostAsync("api/Users/UpdateUser", content);
                }
            }
        }

        public async Task<List<FoodModel>> getAllFood()
        {
            List<FoodModel> listFoodModel = new List<FoodModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(getIPAddress());

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage respone = await client.GetAsync("/api/Food");

                if (respone.IsSuccessStatusCode)
                {
                    var usersTask = respone.Content.ReadAsAsync<List<FoodModel>>();

                    usersTask.Wait();

                    listFoodModel = usersTask.Result;
                }
            }
            return listFoodModel;
        }

        public async Task<List<FoodModel>> searchListFoodByEachRestaurant(string name, List<FoodModel> foodModels)
        {
            List<FoodModel> food = new List<FoodModel>();

            food = foodModels.Where(nameFood => nameFood.NameofFood.ToUpper().Contains(name.ToUpper())).ToList();

            return food;
        }
    }
}
