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
    public class Type_Services : IType_Services
    {
        private readonly IRepository<Types> types;

        public Type_Services(IRepository<Types> types)
        {
            this.types = types;
        }

        //public IEnumerable<Types> GetListOfTypeByID(int? id)
        //{
        //    //return types.Get(id);
        //}

        public IEnumerable<Types> GetAllTypes()
        {
            return types.GetAll().ToArray();
        }

        public Types GetTypeByID(int? id)
        {
            return types.Get(id);
        }

        public void Insert(Types entity)
        {
            types.Insert(entity);
        }

        public void Update(Types entity)
        {
            types.Update(entity);
        }

        public void Delete(Types entity)
        {
            types.Delete(entity);
        }

        public void Remove(Types entity)
        {
            types.Remove(entity);
        }

        public void SaveChanges()
        {
            types.SaveChanges();
        }
    }
}
