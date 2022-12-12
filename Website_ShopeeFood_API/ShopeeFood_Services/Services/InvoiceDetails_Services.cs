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
        private readonly IRepository<InvoiceDetails> repository;
        
        public InvoiceDetails_Services(IRepository<InvoiceDetails> repository)
        {
            this.repository = repository;
        }

        public void SaveChanges()
        {
            repository.SaveChanges();
        }

        public IEnumerable<InvoiceDetails> GetAllInvoiceDetails()
        {
            return repository.GetAll();
        }

        public InvoiceDetails GetInvoiceDetailsByID(int? id)
        {
            return repository.Get(id);
        }

        public void Insert(InvoiceDetails invoiceDetails)
        {
            repository.Insert(invoiceDetails);
        }

        public void Update(InvoiceDetails invoiceDetails)
        {
            repository.Update(invoiceDetails);
        }

        public void Delete(InvoiceDetails invoiceDetails)
        {
            repository.Delete(invoiceDetails);
        }

        public void Remove(InvoiceDetails invoiceDetails)
        {
            repository.Remove(invoiceDetails);
        }

        public IEnumerable<InvoiceDetails> GetListInvoicesDetailByID(int id)
        {
            return repository.getByID(x => x.InvoicesID == id);
        }
    }
}
