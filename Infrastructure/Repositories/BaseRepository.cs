using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public abstract class BaseRepository<TEntity> where TEntity : class
    {
        private readonly DataContext _dataContext;

        public BaseRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public virtual async Task<TEntity> AddToDb(TEntity entity)
        {
            try
            {
                _dataContext.Set<TEntity>().Add(entity);
                await _dataContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return null!;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            try
            {
                var list = await _dataContext.Set<TEntity>().ToListAsync();
                if (list != null)
                    return list;
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return null!;
        }

        public virtual async Task<TEntity> GetOne(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var entity = await _dataContext.Set<TEntity>().FirstOrDefaultAsync(predicate);
                if (entity != null)
                    return entity;
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return null!;
        }

        public virtual async Task<TEntity> UpdateEntity(TEntity newValues, Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var entity = await _dataContext.Set<TEntity>().FirstOrDefaultAsync(predicate);
                if(entity != null)
                {
                    _dataContext.Entry(entity).CurrentValues.SetValues(newValues);
                    await _dataContext.SaveChangesAsync();
                    return entity;
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return null!;
        }

        public virtual async Task<bool> DeleteFromDb(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var entity = await _dataContext.Set<TEntity>().FirstOrDefaultAsync(predicate);
                if(entity != null)
                {
                    _dataContext.Remove(entity);
                    await _dataContext.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return false;
        }

        public virtual async Task<bool> Exists(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var exists = await _dataContext.Set<TEntity>().AnyAsync(predicate);
                if(exists)
                    return true;
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return false;
        }
    }
}
