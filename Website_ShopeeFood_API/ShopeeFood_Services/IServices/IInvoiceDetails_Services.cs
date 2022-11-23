using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood_Services.IServices
{
    public interface IInvoiceDetails_Services 
    {
        public IEnumerable<InvoiceDetails> GetAllInvoiceDetails();

        public InvoiceDetails GetInvoiceDetailsByID(int? id);

        public void Insert(InvoiceDetails entity);

        public void Update(InvoiceDetails entity);

        public void Delete(InvoiceDetails entity);

        public void Remove(InvoiceDetails entity);

        public void SaveChanges();
    }
}
