using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopeeFood_Services.IServices;
using System.Collections.Generic;

namespace Website_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceDetailsController : ControllerBase
    {
        private readonly IInvoiceDetails_Services _invoiceDetailsService;

        public InvoiceDetailsController(IInvoiceDetails_Services invoiceDetailsService)
        {
           this._invoiceDetailsService = invoiceDetailsService;
        }

        [HttpGet("getAllInvoiceDetails")]
        public IEnumerable<InvoiceDetails> getAllInvoiceDetails()
        {
            return _invoiceDetailsService.GetAllInvoiceDetails();
        }

        [HttpPost("insertInvoicesDetails")]
        public void insertInvoicesDetails(InvoiceDetails invoiceDetails)
        {
            _invoiceDetailsService.Insert(invoiceDetails);
        }

        [HttpGet("getListOfInvoicesDetailByIdInvoices/{idInvoices:int}")]
        public IEnumerable<InvoiceDetails> getListOfInvoicesDetailByIdInvoices(int idInvoices)
        {
           return _invoiceDetailsService.GetListInvoicesDetailByID(idInvoices);
        }

    }
}
