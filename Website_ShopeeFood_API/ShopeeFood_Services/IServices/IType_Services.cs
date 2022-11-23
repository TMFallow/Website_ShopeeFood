using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood_Services.IServices
{
    public interface IType_Services
    {
        public IEnumerable<Types> GetAllTypes();

        //public IEnumerable<Types> GetListOfTypeByID(int? id);

        public Types GetTypeByID(int? id);

        public void Insert(Types entity);

        public void Update(Types entity);

        public void Delete(Types entity);

        public void Remove(Types entity);

        public void SaveChanges();
    }
}
