using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories
{
    public class SubscriberRepository : BaseRepository<SubscriberEntity>
    {
        private readonly DataContext _dataContext;
        public SubscriberRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }
    }
}
