using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System;
using Website_ShopeeFood.Models;
using Newtonsoft.Json;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Website_ShopeeFood.Services;

namespace Website_ShopeeFood.Controllers
{
    public class AreasController : Controller
    {
        private readonly IAPIServices IAPIServices;

        public AreasController(IAPIServices iAPIServices)
        {
            this.IAPIServices = iAPIServices;
        }

        [HttpGet]
        public async Task<ActionResult> Areas_Partial()
        {
            List<AreasModel> areas = new List<AreasModel>();

            areas = await IAPIServices.getArea();

            if (areas != null)
            {
                return PartialView("Areas_Partial", areas);
            }

            return NotFound();
        }


        [HttpGet]
        public async Task<IActionResult> NameOfArea_Partial(int AreaId)
        {
            AreaId = int.Parse(HttpContext.Session.GetString("AreaID"));

            AreasModel areas = new AreasModel();

            areas = await IAPIServices.getNameOfArea(AreaId);

            if (areas != null)
            {
                return PartialView("NameOfArea_Partial", areas);
            }
            return NotFound();
        }

        public async Task<AreasModel> GetNameArea(int areaId)
        {
            //AreaId = int.Parse(HttpContext.Session.GetString("AreaID"));

            AreasModel areas = new AreasModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(IAPIServices.getIPAddress());

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage message = await client.GetAsync($"areas/getNameAreas/{areaId}");

                if (message.IsSuccessStatusCode)
                {
                    var areaMessage = message.Content.ReadAsStringAsync().Result;

                    areas = JsonConvert.DeserializeObject<AreasModel>(areaMessage);

                    return areas;
                }
            }
            return null;
        }
    }
}
