using Data;
using ShopeeFood_Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood_Services.IServices
{
    public interface IDetailAreas_Services
    {
        IEnumerable<DetailAreas> GetAllDetailOfAreas();

        DetailAreas? GetDetailArea(int? Id);

        void Insert(DetailAreas entity);

        void Update(DetailAreas entity);

        void Delete(DetailAreas entity);

        void Remove(DetailAreas entity);

        void SaveChanges();

        IEnumerable<DetailAreas> getDetailAreasByID(int AreaId);
    }
}
