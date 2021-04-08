using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassTrackingSystem.Models.Repositoryes
{
    interface IGenericRepository<T> where T:class
    {
        T Get(int id);
        IQueryable<T> GetAll();
        void Create(T newDataObject); 
        void Update(T changedDataObject); 
        void Delete(int id);
    }
}
