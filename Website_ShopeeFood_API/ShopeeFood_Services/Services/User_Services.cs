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
    public class User_Services : IUser_Services
    {
        private readonly IRepository<User> user;

        public User_Services(IRepository<User> user)
        {
            this.user = user;
        }

        public IEnumerable<User> GetAllUser()
        {
            return user.GetAll().ToArray();
        }

        public User GetFoodByID(int? id)
        {
            return user.Get(id);
        }

        public void Insert(User entity)
        {
            user.Insert(entity);
        }

        public void Update(User entity)
        {
            user.Update(entity);
        }

        public void Delete(User entity)
        {
            user.Delete(entity);
        }

        public void Remove(User entity)
        {
            user.Remove(entity);
        }

        public void SaveChanges()
        {
            user.SaveChanges();
        }

        public User getUserByEmail(string email)
        {
            return user.getUserByEmail(x => x.Email == email);
        }

        public User getUserByUsername(string username)
        {
            return user.getUserByUsername(x => x.Username == username);
        }

    }
}
