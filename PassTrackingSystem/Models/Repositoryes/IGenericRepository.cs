using PassTrackingSystem.Models.SeparatorOnThePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassTrackingSystem.Models.Repositoryes
{
    public interface IGenericRepository<T> where T : class
    {
        T Get(int id);
        IQueryable<T> GetAll();
        void Create(T newDataObject);
        void Update(T changedDataObject);
        void Delete(int id);
        PagesDividedList<T> GetPagesDividedList(PageDividorOptions options);
    }
}
