using Data;
using ShopeeFood_Repository.IRepository;
using ShopeeFood_Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood_Services.Services
{
    public class InvoiceDetails_Services : IInvoiceDetails_Services
    {
        private readonly IInvoiceDetails_Services _service;
        
        public InvoiceDetails_Services(IInvoiceDetails_Services _service)
        {
            this._service = _service;
        }

        public void SaveChanges()
        {
            _service.SaveChanges();
        }

        public IEnumerable<InvoiceDetails> GetAllInvoiceDetails()
        {
            return _service.GetAllInvoiceDetails();
        }

        public InvoiceDetails GetInvoiceDetailsByID(int? id)
        {
            return _service.GetInvoiceDetailsByID(id);
        }

        public void Insert(InvoiceDetails invoiceDetails)
        {
            _service.Insert(invoiceDetails);
        }

        public void Update(InvoiceDetails invoiceDetails)
        {
            _service.Update(invoiceDetails);
        }

        public void Delete(InvoiceDetails invoiceDetails)
        {
            _service.Delete(invoiceDetails);
        }

        public void Remove(InvoiceDetails invoiceDetails)
        {
            _service.Remove(invoiceDetails);
        }
    }
}
