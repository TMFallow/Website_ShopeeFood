using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopeeFood_Services.IServices;
using System.Collections;
using System.Collections.Generic;

namespace Website_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoices_Services invoices_Services;

        public InvoiceController(IInvoices_Services invoices_Services)
        {
            this.invoices_Services = invoices_Services;
        }

        [HttpGet("getAllInvoice")]
        public IEnumerable<Invoices> getAllInvoice()
        {
            return invoices_Services.GetAllInvoices();
        }

        [HttpPost("insertInvoice")]
        public void insertInvoice(Invoices invoices)
        {
             invoices_Services.Insert(invoices);
        }

        [HttpGet("getListOfInvoicesByUserID/{userId:int}")]
        public IEnumerable<Invoices> getListOfInvoicesByUserID(int userId)
        {
            return invoices_Services.getListOfInvoicesByUserID(userId);
        }
    }
}
