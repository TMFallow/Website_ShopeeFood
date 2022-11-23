using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using Website_ShopeeFood.Models;
using Microsoft.AspNetCore.Http;

namespace Website_ShopeeFood.Controllers
{
    public class FoodController : Controller
    {
        string Baseurl = "https://localhost:5001/";
        public IActionResult Index()
        {
            return View();
        }

        List<FoodModel> foods = new List<FoodModel>();


        //Hiển Thị Danh Mục Sản Phẩm Có Trong Quán
        [HttpGet]
        public async Task<ActionResult> TypesofFoods_Partial(int restaurantId)
        {
            if(restaurantId == 0)
            {
                restaurantId = int.Parse(HttpContext.Session.GetString("restaurantId"));
            }

            List<FoodModel> types = new List<FoodModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl); //Chuyển URL

                client.DefaultRequestHeaders.Clear();

                //Định dạng format dữ liệu là JSon

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                // Gửi yêu cầu tìm dịch vụ Web API bằng cách sử dụng HttpClient

                HttpResponseMessage message = await client.GetAsync("api/food/getAllRestaurantUsingIDRestaurant/" + restaurantId + "");


                //Kiểm tra xem có thành không ko

                if (message.IsSuccessStatusCode)
                {
                    //Lưu trữ phản hồi sau ghi gọi api
                    var foodMessage = message.Content.ReadAsAsync<IEnumerable<FoodModel>>();

                    foodMessage.Wait();

                    var dsFood = foodMessage.Result;

                    foreach(var item in dsFood)
                    {
                        foods.Add(item);
                    }

                    return PartialView("TypesofFoods_Partial", foods);
                }
            }
            return NotFound();
        }
    }
}
