using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassTrackingSystem.Models.Repositoryes
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected ApplicationDBContext context;

        public GenericRepository(ApplicationDBContext context)
        {
            this.context = context;
        }
        public virtual T Get(int id)
        {
            return context.Set<T>().Find(id);
        }

        public virtual IQueryable<T> GetAll()
        {
            return context.Set<T>();
        }

        public virtual void Create(T newDataObject)
        {
            context.Add(newDataObject);
            context.SaveChanges();
        }

        public virtual void Delete(int id)
        {
            context.Remove(Get(id));
            context.SaveChanges();
        }

        public virtual void Update(T changedDataObject)
        {
            context.Update<T>(changedDataObject); 
            context.SaveChanges();
        }
    }
}