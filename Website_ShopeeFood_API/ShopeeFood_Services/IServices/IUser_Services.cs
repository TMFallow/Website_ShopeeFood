using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood_Services.IServices
{
    public interface IUser_Services
    {
        public IEnumerable<User> GetAllUser();

        public User GetFoodByID(int? id);

        public void Insert(User entity);

        public void Update(User entity);

        public void Delete(User entity);

        public void Remove(User entity);

        public void SaveChanges();
    }
}
