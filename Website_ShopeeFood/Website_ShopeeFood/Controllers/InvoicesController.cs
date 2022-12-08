using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Website_ShopeeFood.Models;
using Website_ShopeeFood.Services;

namespace Website_ShopeeFood.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly IAPIServices aPIServices;

        public InvoicesController(IAPIServices aPIServices)
        {
            this.aPIServices = aPIServices;
        }

        public async Task<IActionResult> ListInvoices()
        {
            List<InvoicesModel> listInvoices = new List<InvoicesModel>();
            if (HttpContext.Session.GetString("UserIdToCheckInvoices") != null)
            {
                int userID = int.Parse(HttpContext.Session.GetString("UserIdToCheckInvoices"));
                listInvoices = await aPIServices.getListInvoicesByUserID(userID);
            }
            return View(listInvoices);
        }

        public async Task<IActionResult> addNewInvoices(InvoicesModel invoicesModel)
        {

            return RedirectToAction("ListInvoices");
        }
    }
}
