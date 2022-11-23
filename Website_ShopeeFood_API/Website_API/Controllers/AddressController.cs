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
    public class AddressController : ControllerBase
    {
        private readonly IAddress_Services address;

        private readonly ILogger<AddressController> logger;

        public AddressController(IAddress_Services address, ILogger<AddressController> logger)
        {
            this.address = address;
            this.logger = logger;
        }

        [HttpGet]
        public IEnumerable<AddressToDelivery> GetAddress()
        {
            return address.GetAllAddress().ToArray();
        }
    }
}
