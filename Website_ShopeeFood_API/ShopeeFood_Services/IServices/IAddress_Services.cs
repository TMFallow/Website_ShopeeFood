using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood_Services.IServices
{
    public interface IAddress_Services
    {
        public IEnumerable<AddressToDelivery> GetAllAddress();

        public AddressToDelivery GetAddressByID(int? id);

        public void Insert(AddressToDelivery entity);

        public void Update(AddressToDelivery entity);

        public void Delete(AddressToDelivery entity);

        public void Remove(AddressToDelivery entity);

        public void SaveChanges();
    }
}
