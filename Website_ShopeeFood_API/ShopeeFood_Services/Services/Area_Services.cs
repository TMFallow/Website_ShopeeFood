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
    public class Area_Services : IArea_Services
    {
        private readonly IRepository<Areas> areas_Repository;

        public Area_Services(IRepository<Areas> areas_Repository)
        {
            this.areas_Repository = areas_Repository;
        }

        public IEnumerable<Areas> GetAllArea()
        {
            return areas_Repository.GetAll();
        }

        public Areas GetArea(int? id)
        {
            return areas_Repository.Get(id);
        }

        public void Insert(Areas entity)
        {
            areas_Repository.Insert(entity);
        }

        public void Update(Areas entity)
        {
            areas_Repository.Update(entity);
        }

        public void Delete(Areas entity)
        {
            areas_Repository.Delete(entity);
        }

        public void Remove(Areas entity)
        {
            areas_Repository.Remove(entity);
        }

        public void SaveChanges()
        {
            areas_Repository.SaveChanges();
        }
    }
}
