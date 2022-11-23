using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using Website_ShopeeFood.Models;
using System.Collections.Generic;
using NuGet.Packaging.Signing;

namespace Website_ShopeeFood.Controllers
{
    public class DetailAreasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        string Baseurl = "https://localhost:5001/";

        List<DetailAreasModel> model = new List<DetailAreasModel>();

        public async Task<IActionResult> getDistrics_Partial(int AreaID)
        {
            if(AreaID == 0)
            {
                if(int.Parse(HttpContext.Session.GetString("AreaIDofRestaurant")) == 0)
                {
                    AreaID = 1;
                }
                else
                {
                    AreaID = int.Parse(HttpContext.Session.GetString("AreaIDofRestaurant"));
                }
            }


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage message = await client.GetAsync("api/Detailareas/getDetailAreasByID/" + AreaID + "");

                if (message.IsSuccessStatusCode)
                {
                    var detailAreaMessage = message.Content.ReadAsAsync<IEnumerable<DetailAreasModel>>();

                    detailAreaMessage.Wait();

                    var listDistrict = detailAreaMessage.Result;

                    foreach (var item in listDistrict)
                    {
                        model.Add(item);
                    }

                    return PartialView("getDistrics_Partial", model);
                }
            }
            return NotFound();
        }
    }
}
