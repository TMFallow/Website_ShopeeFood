using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood_Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        T Get(int? id);

        void Insert(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Remove(T entity);

        void SaveChanges();

        IEnumerable<T> getByID(Func<T, bool> entity);

        IEnumerable<T> getByIdType(Func<T, bool> entity);

        IEnumerable<T> getByIdDistricts(Func<T, bool> entity);

<<<<<<< HEAD
        T getUserByEmail(Func<T, bool> entity);
=======
        IEnumerable<T> getByIdTypes(Func<T, bool> entity);
>>>>>>> 9e422fae9355731d14a7053a24af458c3604b02f
    }
}
