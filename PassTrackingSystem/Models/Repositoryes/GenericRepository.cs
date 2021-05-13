using PassTrackingSystem.Models.SeparatorOnThePage;
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
        public virtual ValueTask<T> Get(int id)
        {
            return context.Set<T>().FindAsync(id);
        }

        public virtual IQueryable<T> GetAll()
        {
            return context.Set<T>();
        }

        public virtual Task Create(T newDataObject)
        {
            context.Add(newDataObject);
            return context.SaveChangesAsync();
        }

        public virtual Task Delete(int id)
        {
            context.Remove(Get(id));
            return context.SaveChangesAsync();
        }

        public virtual Task Update(T changedDataObject)
        {
            context.Update<T>(changedDataObject); 
            return context.SaveChangesAsync();
        }
    
    }
}