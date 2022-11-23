using Data;
using ShopeeFood_Data.Model;
using ShopeeFood_Repository.IRepository;
using ShopeeFood_Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood_Services.Services
{
    public class Restaurant_Services : IRestaurant_Services
    {
        private readonly IRepository<Restaurant> restaurant;

        public Restaurant_Services(IRepository<Restaurant> restaurant)
        {
            this.restaurant = restaurant;
        }

        public IEnumerable<Restaurant> GetAllRestaurant()
        {
            return restaurant.GetAll();
        }

        public Restaurant GetRestaurantByID(int? id)
        {
            return restaurant.Get(id);
        }

        public void Insert(Restaurant entity)
        {
            restaurant.Insert(entity);
        }

        public void Update(Restaurant entity)
        {
            restaurant.Update(entity);
        }

        public void Delete(Restaurant entity)
        {
            restaurant.Delete(entity);
        }

        public void Remove(Restaurant entity)
        {
            restaurant.Remove(entity);
        }

        public void SaveChanges()
        {
            restaurant.SaveChanges();
        }

        public IEnumerable<Restaurant> GetListRestaurantsbyID(int Id)
        {
            return restaurant.getByID(x => x.RestaurantID == Id);
        }

        public IEnumerable<Restaurant> getListOfRestaurantByIdDistricts(int IDDetailsArea)
        {
            return restaurant.getByIdDistricts(x => x.IDDetailsArea == IDDetailsArea);
        }
    }
}
