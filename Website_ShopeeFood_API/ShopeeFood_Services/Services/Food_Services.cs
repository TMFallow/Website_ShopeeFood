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
    public class Food_Services : IFood_Services
    {
        private readonly IRepository<Foods> foods;

        public Food_Services(IRepository<Foods> foods)
        {
            this.foods = foods;
        }

        public IEnumerable<Foods> GetAllFoods()
        {
            return foods.GetAll();
        }

        public Foods GetFoodByID(int? id)
        {
            return foods.Get(id);
        }

        public void Insert(Foods entity)
        {
            foods.Insert(entity);
        }

        public void Update(Foods entity)
        {
            foods.Update(entity);
        }

        public void Delete(Foods entity)
        {
            foods.Delete(entity);
        }

        public void Remove(Foods entity)
        {
            foods.Remove(entity);
        }

        public void SaveChanges()
        {
            foods.SaveChanges();
        }

        public IEnumerable<Foods> getByID(int Id)
        {
            return foods.getByID(x => x.RestaurantID == Id);
        }

        public IEnumerable<Foods> getByIdType(int TypeofFood)
        {
            return foods.getByIdType(x=>x.TypeofFood == TypeofFood.ToString());
        }
    }
}
