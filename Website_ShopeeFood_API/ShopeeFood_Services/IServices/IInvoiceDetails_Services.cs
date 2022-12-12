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
        IEnumerable<InvoiceDetails> GetAllInvoiceDetails();

        InvoiceDetails GetInvoiceDetailsByID(int? id);

        void Insert(InvoiceDetails entity);

        void Update(InvoiceDetails entity);

        void Delete(InvoiceDetails entity);

        void Remove(InvoiceDetails entity);

        void SaveChanges();

        IEnumerable<InvoiceDetails> GetListInvoicesDetailByID(int id);
    }
}
