using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood_Services.IServices
{
    public interface IInvoices_Services
    {
        public IEnumerable<Invoices> GetAllInvoices();

        public Invoices GetInvoiceByID(int? id);

        public void Insert(Invoices entity);

        public void Update(Invoices entity);

        public void Delete(Invoices entity);

        public void Remove(Invoices entity);

        public void SaveChanges();
    }
}
