using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using Website_ShopeeFood.Models;
using System.Collections.Generic;
using NuGet.Packaging.Signing;
using Website_ShopeeFood.Services;

namespace Website_ShopeeFood.Controllers
{
    public class DetailAreasController : Controller
    {
        private readonly IAPIServices aPIServices;

        public DetailAreasController(IAPIServices aPIServices)
        {
            this.aPIServices = aPIServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        List<DetailAreasModel> model = new List<DetailAreasModel>();

        public async Task<IActionResult> getDistrics_Partial(int AreaID)
        {
            if (AreaID == 0)
            {
                if (int.Parse(HttpContext.Session.GetString("AreaIDofRestaurant")) == 0)
                {
                    AreaID = 1;
                }
                else
                {
                    AreaID = int.Parse(HttpContext.Session.GetString("AreaIDofRestaurant"));
                }
            }


            model = await aPIServices.getDetailAreas(AreaID);

            if (model != null)
            {
                return PartialView("getDistrics_Partial", model);
            }
            return NotFound();
        }
    }
}
