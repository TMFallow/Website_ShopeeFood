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
    public class Promotion_Services : IPromotion_Services
    {

        private readonly IRepository<Promotion> promotions;

        public Promotion_Services(IRepository<Promotion> promotions)
        {
            this.promotions = promotions;
        }

        public IEnumerable<Promotion> GetAllPromotion()
        {
            return promotions.GetAll().ToArray();
        }

        public Promotion GetPromotionByID(int? id)
        {
            return promotions.Get(id);
        }

        public void Insert(Promotion entity)
        {
            promotions.Insert(entity);
        }

        public void Update(Promotion entity)
        {
            promotions.Update(entity);
        }

        public void Delete(Promotion entity)
        {
            promotions.Delete(entity);
        }

        public void Remove(Promotion entity)
        {
            promotions.Remove(entity);
        }

        public void SaveChanges()
        {
            promotions.SaveChanges();
        }
    }
}
