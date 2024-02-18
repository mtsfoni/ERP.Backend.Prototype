using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ERP.Backend.Models
{
    public class EntityFrameworkRepository<T>
        (ApplicationDbContext context) 
        : IRepository<T> where T : class
    {
        private readonly DbSet<T> dbSet = context.Set<T>();

        public ValueTask<T?> GetById(int id) => dbSet.FindAsync(id);        


        public async Task<List<T>> GetAll()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<List<T>> GetWhere(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.Where(predicate).ToListAsync();
        }

        public async Task Add(T entity)
        {
            dbSet.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            dbSet.Update(entity);            
            await context.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            dbSet.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
