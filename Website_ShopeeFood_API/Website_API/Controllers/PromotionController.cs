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
    public class PromotionController : ControllerBase
    {
        private readonly IPromotion_Services promotion;

        private readonly ILogger<PromotionController> logger;

        public PromotionController(IPromotion_Services promotion, ILogger<PromotionController> logger)
        {
            this.promotion = promotion;
            this.logger = logger;
        }

        [HttpGet]
        public IEnumerable<Promotion> GetArea()
        {
            return promotion.GetAllPromotion().ToArray();
        }
    }
}
