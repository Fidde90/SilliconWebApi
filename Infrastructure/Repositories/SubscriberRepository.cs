using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories
{
    public class SubscriberRepository(DataContext dataContext) : BaseRepository<SubscriberEntity>(dataContext)
    {
        private readonly DataContext _dataContext = dataContext;
    }
}
