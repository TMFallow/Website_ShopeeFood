using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopeeFood_Data.Model;
using ShopeeFood_Services.IServices;
using System.Collections.Generic;
using System.Linq;

namespace Website_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailAreasController : ControllerBase
    {
        private readonly IDetailAreas_Services detailAreas;

        private readonly ILogger<DetailAreasController> logger;

        public DetailAreasController(IDetailAreas_Services detailAreas, ILogger<DetailAreasController> logger)
        {
            this.detailAreas = detailAreas;
            this.logger = logger;
        }

        [HttpGet("getDetailAreasByID/{AreaId:int}")]
        public IEnumerable<DetailAreas> getDetailAreasByID(int AreaId)
        {
            return detailAreas.getDetailAreasByID(AreaId).ToList();
        }
    }
}
