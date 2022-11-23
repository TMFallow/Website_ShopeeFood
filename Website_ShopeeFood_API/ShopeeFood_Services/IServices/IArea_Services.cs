using Data;

namespace ShopeeFood_Services.IServices
{
    public interface IArea_Services
    {
        IEnumerable<Areas> GetAllArea();

        Areas? GetArea(int? Id);

        void Insert(Areas entity);

        void Update(Areas entity);

        void Delete(Areas entity);

        void Remove(Areas entity);

        void SaveChanges();
    }
}