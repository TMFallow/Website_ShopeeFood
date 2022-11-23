using Data;
using ShopeeFood_Data.Model;
using ShopeeFood_Repository.IRepository;
using ShopeeFood_Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood_Services.Services
{
    public class DetailAreas_Services : IDetailAreas_Services
    {
        private readonly IRepository<DetailAreas> areas_Repository;

        public DetailAreas_Services(IRepository<DetailAreas> areas_Repository)
        {
            this.areas_Repository = areas_Repository;
        }

        public IEnumerable<DetailAreas> GetAllDetailOfAreas()
        {
            return areas_Repository.GetAll();
        }

        public DetailAreas GetDetailArea(int? id)
        {
            return areas_Repository.Get(id);
        }

        public void Insert(DetailAreas entity)
        {
            areas_Repository.Insert(entity);
        }

        public void Update(DetailAreas entity)
        {
            areas_Repository.Update(entity);
        }

        public void Delete(DetailAreas entity)
        {
            areas_Repository.Delete(entity);
        }

        public void Remove(DetailAreas entity)
        {
            areas_Repository.Remove(entity);
        }

        public void SaveChanges()
        {
            areas_Repository.SaveChanges();
        }

        public IEnumerable<DetailAreas> getDetailAreasByID(int AreaId)
        {
            return areas_Repository.getByID(x => x.AreaID == AreaId);
        }
    }
}
