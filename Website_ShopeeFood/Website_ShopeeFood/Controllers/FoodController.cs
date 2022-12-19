using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using Website_ShopeeFood.Models;
using Microsoft.AspNetCore.Http;
using Website_ShopeeFood.Services;

namespace Website_ShopeeFood.Controllers
{
    public class FoodController : Controller
    {
        private readonly IAPIServices aPIServices;

        static bool checkingSearchFood = false;

        public FoodController(IAPIServices aPIServices)
        {
            this.aPIServices = aPIServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        static List<FoodModel> foods = new List<FoodModel>();


        //Hiển Thị Danh Mục Sản Phẩm Có Trong Quán
        [HttpGet]
        public async Task<ActionResult> TypesofFoods_Partial(int restaurantId)
        {
            if (restaurantId == 0)
            {
                restaurantId = int.Parse(HttpContext.Session.GetString("restaurantId"));
            }

            if (checkingSearchFood == false)
            {
                foods = await aPIServices.getAllFoodByIdRestaurant(restaurantId);
            }

            if (foods != null)
            {

                return PartialView("TypesofFoods_Partial", foods);

            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> searchingOfFood(string nameOfFood)
        {
            int restaurantId = 0;

            checkingSearchFood = true;

            if (nameOfFood == null)
            {
                return BadRequest();
            }

            if (int.Parse(HttpContext.Session.GetString("restaurantId")) != 0)
            {
                restaurantId = int.Parse(HttpContext.Session.GetString("restaurantId"));
            }

            List<FoodModel> foodModels = new List<FoodModel>();

            foodModels = await aPIServices.getAllFoodByIdRestaurant(restaurantId);

            foods = await aPIServices.searchListFoodByEachRestaurant(nameOfFood, foodModels);

            return RedirectToAction("DetailOfRestaurant", "Restaurant");
        }

        [HttpGet]
        public IActionResult resetTheListFood()
        {
            checkingSearchFood = false;
            return RedirectToAction("DetailOfRestaurant", "Restaurant");
        }
    }
}
