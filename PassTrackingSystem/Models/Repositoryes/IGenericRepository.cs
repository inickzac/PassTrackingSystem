using PassTrackingSystem.Models.SeparatorOnThePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassTrackingSystem.Models.Repositoryes
{
    public interface IGenericRepository<T> where T : class
    {
        ValueTask<T> Get(int id);
        IQueryable<T> GetAll();
        Task Create(T newDataObject);
        Task Update(T changedDataObject);
        Task Delete(int id);
    }
}
