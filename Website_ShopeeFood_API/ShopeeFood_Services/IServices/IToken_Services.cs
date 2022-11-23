using Data;
using ShopeeFood_Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood_Services.IServices
{
    public interface IToken_Services
    {
        public IEnumerable<Token> GetAllTokens();

        public Token GetFoodByID(int? id);

        public void Insert(Token entity);

        public void Update(Token entity);

        public void Delete(Token entity);

        public void Remove(Token entity);

        public void SaveChanges();
    }
}
