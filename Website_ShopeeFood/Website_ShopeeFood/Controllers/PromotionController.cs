using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using Website_ShopeeFood.Models;

namespace Website_ShopeeFood.Controllers
{
    public class PromotionController : Controller
    {
        string Baseurl = "https://localhost:5001/";

        [HttpGet]
        public async Task<ActionResult> Promotion_Partial()
        {
            List<PromotionModels> promotions = new List<PromotionModels>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl); 

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage message = await client.GetAsync("api/promotion");

                if (message.IsSuccessStatusCode)
                {
                    var promotionsMessage = message.Content.ReadAsStringAsync().Result;

                    promotions = JsonConvert.DeserializeObject<List<PromotionModels>>(promotionsMessage);

                    var dsKhuyenMai = new List<PromotionModels>(); 

                    for(int i = 1; i < promotions.Count; i++)
                    {
                        dsKhuyenMai.Add(promotions[i]);
                    }

                    return PartialView("Promotion_Partial", dsKhuyenMai);
                }
            }
            return NotFound();
        }
    }
}
