using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Website_ShopeeFood.Models;
using Website_ShopeeFood.Services;

namespace Website_ShopeeFood.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly IAPIServices aPIServices;

        public List<ItemsModel> listFoodsAddingToCart;

        static List<InvoiceDetailsModel> listInvoiceDetailsModel = new List<InvoiceDetailsModel>();

        static List<InvoicesDetailByIDModel> invoicesDetailByIDModels = new List<InvoicesDetailByIDModel>();

        public InvoicesController(IAPIServices aPIServices)
        {
            this.aPIServices = aPIServices;
            this.listFoodsAddingToCart = RestaurantController.listCart;
        }

        public async Task<IActionResult> ListInvoices()
        {
            List<InvoicesModel> listInvoices = new List<InvoicesModel>();
            if (HttpContext.Session.GetString("UserIdToCheckInvoices") != null)
            {
                int userID = int.Parse(HttpContext.Session.GetString("UserIdToCheckInvoices"));
                listInvoices = await aPIServices.getListInvoicesByUserID(userID);
            }

            if (TempData["Invoices"] != null)
            {
                ViewBag.AddingInvoices = TempData["Invoices"] as string;
            }

            ViewData["ViewDataInvoiceDetails"] = listInvoiceDetailsModel;

            return View(listInvoices);
        }

        public async Task<IActionResult> addNewInvoices(double totalPrice)
        {
            string status = "On way to delivery!";

            string detail = "";

            int userId = int.Parse(HttpContext.Session.GetString("UserIdToCheckInvoices"));

            DateTime dateTime = DateTime.Now;

            InvoicesModel invoicesModel = new InvoicesModel()
            {
                DeliveryDate = dateTime,
                Status = status,
                Details = detail,
                UserID = userId,
                TotalPrices = totalPrice,
            };

            if (invoicesModel != null)
            {
                aPIServices.insertInvoices(invoicesModel);

                int invoicesId = 1;

                List<InvoicesModel> listInvoicesModel = new List<InvoicesModel>();

                listInvoicesModel = await aPIServices.getAllInvoices();

                if(listInvoicesModel !=null)
                {
                    for (int i = 0; i < listInvoicesModel.Count; i++)
                    {
                        invoicesId = listInvoicesModel[i].InvoicesID;
                    }
                }

                HttpContext.Session.SetString("IdInvoices", invoicesId.ToString());

                TempData["Invoices"] = "Adding Invoices Successfully!";
            }
            else
            {
                TempData["Invoices"] = "Try Again!";
            }

            InvoiceDetailsModel InvoiceDetailsModel = new InvoiceDetailsModel(); 
            List<InvoiceDetailsModel> listInvoiceDetailsModel = new List<InvoiceDetailsModel>();

            if (listFoodsAddingToCart != null)
            {
                foreach (var item in listFoodsAddingToCart)
                {
                    InvoiceDetailsModel.InvoicesID = int.Parse(HttpContext.Session.GetString("IdInvoices"));
                    InvoiceDetailsModel.FoodId = item.Food.FoodId;
                    InvoiceDetailsModel.NameofFood = item.Food.NameofFood;
                    InvoiceDetailsModel.Images = item.Food.Images;
                    InvoiceDetailsModel.Price = item.Food.Price;
                    InvoiceDetailsModel.Numbers = item.Quantity;

                    aPIServices.insertInvoiceDetails(InvoiceDetailsModel);

                    listInvoiceDetailsModel.Add(InvoiceDetailsModel);
                }
            }

            invoicesDetailByIDModels.Add(new InvoicesDetailByIDModel { invoicesModel = invoicesModel, invoiceDetails = listInvoiceDetailsModel });

            return RedirectToAction("ListInvoices");
        }

        [HttpGet]
        public async Task<IActionResult> getListOfInvoicesDetailByInvoiceId(int invoicesId)
        {
            listInvoiceDetailsModel.Clear();
            listInvoiceDetailsModel = await aPIServices.getListDetailsInvoiceByInvoices(invoicesId);
            return RedirectToAction("ListInvoices", "Invoices");
        }

        public IActionResult _listDetailInvoices()
        {
            return PartialView("_listDetailInvoices", listInvoiceDetailsModel);
        }
    }
}
