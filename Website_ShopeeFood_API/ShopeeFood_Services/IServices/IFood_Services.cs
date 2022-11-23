using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood_Services.IServices
{
    public interface IFood_Services
    {
        public IEnumerable<Foods> GetAllFoods();

        public Foods GetFoodByID(int? id);

        public void Insert(Foods entity);

        public void Update(Foods entity);

        public void Delete(Foods entity);

        public void Remove(Foods entity);

        public void SaveChanges();

        IEnumerable<Foods> getByID(int Id);

        IEnumerable<Foods> getByIdType(int getByIdType);
    }
}
