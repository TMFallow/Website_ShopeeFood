using Data;
using ShopeeFood_Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood_Services.Services
{
    public class InvoiceDetails
    {
        private readonly IRepository<InvoiceDetails> invoiceDetails;

        public InvoiceDetails(IRepository<InvoiceDetails> invoiceDetails)
        {
            this.invoiceDetails = invoiceDetails;
        }

        public IEnumerable<InvoiceDetails> GetAllInvoiceDetails()
        {
            return invoiceDetails.GetAll().ToArray();
        }

        public InvoiceDetails GetInvoiceDetailsByID(int? id)
        {
            return invoiceDetails.Get(id);
        }

        public void Insert(InvoiceDetails entity)
        {
            invoiceDetails.Insert(entity);
        }

        public void Update(InvoiceDetails entity)
        {
            invoiceDetails.Update(entity);
        }

        public void Delete(InvoiceDetails entity)
        {
            invoiceDetails.Delete(entity);
        }

        public void Remove(InvoiceDetails entity)
        {
            invoiceDetails.Remove(entity);
        }

        public void SaveChanges()
        {
            invoiceDetails.SaveChanges();
        }
    }
}
