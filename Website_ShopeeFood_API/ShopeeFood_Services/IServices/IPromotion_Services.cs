using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood_Services.IServices
{
    public interface IPromotion_Services
    {
        public IEnumerable<Promotion> GetAllPromotion();

        public Promotion GetPromotionByID(int? id);

        public void Insert(Promotion entity);

        public void Update(Promotion entity);

        public void Delete(Promotion entity);

        public void Remove(Promotion entity);

        public void SaveChanges();
    }
}
