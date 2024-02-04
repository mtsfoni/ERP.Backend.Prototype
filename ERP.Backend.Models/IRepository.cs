using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Backend.Models
{
    public interface IRepository<T>
    {
        ValueTask<T?> GetById(int id);
        Task<List<T>> GetAll();
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task<List<T>> GetWhere(Expression<Func<T, bool>> predicate);
    }
}
