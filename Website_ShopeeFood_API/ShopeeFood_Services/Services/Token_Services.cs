using Data;
using ShopeeFood_Data.Model;
using ShopeeFood_Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood_Services.Services
{
    public class Token_Services
    {
        private readonly IRepository<Token> token;

        public Token_Services(IRepository<Token> token)
        {
            this.token = token;
        }

        public IEnumerable<Token> GetAllTokens()
        {
            return token.GetAll().ToArray();
        }

        public Token GetFoodByID(int? id)
        {
            return token.Get(id);
        }

        public void Insert(Token entity)
        {
            token.Insert(entity);
        }

        public void Update(Token entity)
        {
            token.Update(entity);
        }

        public void Delete(Token entity)
        {
            token.Delete(entity);
        }

        public void Remove(Token entity)
        {
            token.Remove(entity);
        }

        public void SaveChanges()
        {
            token.SaveChanges();
        }
    }
}
