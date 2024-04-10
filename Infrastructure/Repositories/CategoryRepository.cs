using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infrastructure.Repositories
{
    public class CategoryRepository(DataContext dataContext) : BaseRepository<CategoryEntity>(dataContext) 
    {
        private readonly DataContext _dataContext = dataContext;


        /// <summary>
        ///     Gets all categories from the database, as a IEnumerable list.
        /// </summary>
        /// <returns>returns an orderd list by categoryname</returns>
        public async override Task<IEnumerable<CategoryEntity>> GetAll()
        {
            try
            {
                var list = await _dataContext.Set<CategoryEntity>().OrderBy(o => o.CategoryName).ToListAsync();
                if (list.Count > 0)
                    return list;
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return null!;
        }
    }
}
