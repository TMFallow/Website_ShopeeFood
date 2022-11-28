using Data;
using ShopeeFood_Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood_Services.IServices
{
    public interface IRestaurant_Services
    {
        IEnumerable<Restaurant> GetAllRestaurant();

        public Restaurant GetRestaurantByID(int? id);

        public void Insert(Restaurant entity);

        public void Update(Restaurant entity);

        public void Delete(Restaurant entity);

        public void Remove(Restaurant entity);

        public void SaveChanges();

        IEnumerable<Restaurant> GetListRestaurantsbyID(int Id);

        IEnumerable<Restaurant> getListOfRestaurantByIdDistricts(int IDDetailsArea);

    }
}
