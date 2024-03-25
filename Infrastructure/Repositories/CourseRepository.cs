using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories
{
    public class CourseRepository(DataContext dataContext) : BaseRepository<CourseEntity>(dataContext)
    {
        private readonly DataContext _dataContext = dataContext;
    }
}
