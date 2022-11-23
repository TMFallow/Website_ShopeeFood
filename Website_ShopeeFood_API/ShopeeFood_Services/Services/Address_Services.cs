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
    public class Address_Services : IAddress_Services
    {

        private readonly IRepository<AddressToDelivery> addressToDelivery;

        public Address_Services(IRepository<AddressToDelivery> addressToDelivery)
        {
            this.addressToDelivery = addressToDelivery;
        }


        public IEnumerable<AddressToDelivery> GetAllAddress()
        {
            return addressToDelivery.GetAll();
        }

        public AddressToDelivery GetAddressByID(int? id)
        {
            return addressToDelivery.Get(id);
        }

        public void Insert(AddressToDelivery entity)
        {
            addressToDelivery.Insert(entity);
        }

        public void Update(AddressToDelivery entity)
        {
            addressToDelivery.Update(entity);
        }

        public void Delete(AddressToDelivery entity)
        {
            addressToDelivery.Delete(entity);
        }

        public void Remove(AddressToDelivery entity)
        {
            addressToDelivery.Remove(entity);
        }

        public void SaveChanges()
        {
            addressToDelivery.SaveChanges();
        }
    }
}
