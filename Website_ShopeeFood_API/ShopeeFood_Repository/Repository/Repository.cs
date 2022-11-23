using Data;
using Microsoft.EntityFrameworkCore;
using ShopeeFood_Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood_Repository.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DbSet<T> entities;

        private readonly Application_Context context;

        public Repository(Application_Context context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity");
            }
            else
            {
                entities.Remove(entity);
                context.SaveChanges();
            }
        }


        public T Get(int? id)
        {
            return entities.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity");
            }
            else
            {
                entities.Add(entity);
                context.SaveChanges();
            }
        }

        public void Remove(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity");
            }
            entities.Remove(entity);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity");
            }
            context.SaveChanges();
        }

        public T GetByPredicate(Func<T, bool> func)
        {
            return entities.FirstOrDefault(func);
        }

        public IEnumerable<T> getByID(Func<T, bool> entity)
        {
            return entities.Where(entity).ToList();
        }

        public IEnumerable<T> getByIdType(Func<T, bool> entity)
        {
            return entities.Where(entity).ToList();
        }

        public IEnumerable<T> getByIdDistricts(Func<T, bool> entity)
        {
            return entities.Where(entity).ToList();
        }
    }
}
