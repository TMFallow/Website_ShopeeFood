using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopeeFood_Services.IServices;
using System.Collections.Generic;
using System.Linq;

namespace Website_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreasController : ControllerBase
    {
        private readonly IArea_Services area;

        private readonly ILogger<AreasController> logger;

        public AreasController(IArea_Services area, ILogger<AreasController> logger)
        {
            this.area = area;
            this.logger = logger;
        }

        [HttpGet]
        public IEnumerable<Areas> GetArea()
        {
            return area.GetAllArea().ToArray();
        }

        [HttpGet("getNameAreas/{AreaId:int}")]
        public Areas getNameAreas(int AreaId)
        {
            return area.GetArea(AreaId);
        }
    }
}
