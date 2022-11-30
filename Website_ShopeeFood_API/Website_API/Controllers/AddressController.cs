using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopeeFood_Services.IServices;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

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

        [HttpGet("GetAddressToDelivery/{Id:int}")]
        public AddressToDelivery GetAddressToDelivery(int Id)
        {
            return address.GetAddressByID(Id);
        }

        [HttpGet("getListAddressByID/{userId:int}")]
        public IEnumerable<AddressToDelivery> getListAddressByID(int userId)
        {
            return address.getListAddressById(userId);
        }

        [HttpPost("UpdateAddressUser")]
        public void UpdateAddressUser(AddressToDelivery addressToDelivery)
        {
            address.Update(addressToDelivery);
        }
    }
}
