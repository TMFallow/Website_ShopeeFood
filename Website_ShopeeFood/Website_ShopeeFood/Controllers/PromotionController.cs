using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using Website_ShopeeFood.Models;
using Website_ShopeeFood.Services;

namespace Website_ShopeeFood.Controllers
{
    public class PromotionController : Controller
    {
        private readonly IAPIServices _APIServices;

        public PromotionController(IAPIServices aPIServices)
        {
            this._APIServices = aPIServices;
        }

        [HttpGet]
        public async Task<ActionResult> Promotion_Partial()
        {
            List<PromotionModels> promotions = new List<PromotionModels>();

            promotions = await _APIServices.getPromotion();

            if (promotions != null)
            {
                return PartialView("Promotion_Partial", promotions);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
