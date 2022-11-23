using Data;
using ShopeeFood_Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood_Services.Services
{
    public class Invoices_Services
    {
        private readonly IRepository<Invoices> invoices;

        public Invoices_Services(IRepository<Invoices> invoices)
        {
            this.invoices = invoices;
        }

        public IEnumerable<Invoices> GetAllInvoices()
        {
            return invoices.GetAll().ToArray();
        }

        public Invoices GetInvoiceByID(int? id)
        {
            return invoices.Get(id);
        }

        public void Insert(Invoices entity)
        {
            invoices.Insert(entity);
        }

        public void Update(Invoices entity)
        {
            invoices.Update(entity);
        }

        public void Delete(Invoices entity)
        {
            invoices.Delete(entity);
        }

        public void Remove(Invoices entity)
        {
            invoices.Remove(entity);
        }

        public void SaveChanges()
        {
            invoices.SaveChanges();
        }
    }
}
