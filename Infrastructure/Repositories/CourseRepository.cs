using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infrastructure.Repositories
{
    public class CourseRepository(DataContext dataContext) : BaseRepository<CourseEntity>(dataContext)
    {
        private readonly DataContext _dataContext = dataContext;

        public async override Task<IEnumerable<CourseEntity>> GetAll()
        {
            try
            {
                var query = _dataContext.Courses.Include(c => c.Category).AsQueryable(); //inkluderar categorierna och gör den frågbar
                query = query.OrderByDescending(o => o.LastUpdated); // sorterar den efter senast uppdaterad
                var courses = await query.ToListAsync(); //blir sedan en lista
                if (courses.Count > 0)
                    return courses;
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return null!;
        }
    }
}
