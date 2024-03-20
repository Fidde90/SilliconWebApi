using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Infrastructure.Services
{
    public class SubscriberService
    {
        private readonly SubscriberRepository _subscriberRepository;

        public SubscriberService(SubscriberRepository repository)
        {
            _subscriberRepository = repository;
        }

        public async Task<SubscriberEntity> CreateSubscriberAsync(string email)
        {
            try
            {
                if (!await _subscriberRepository.Exists(x => x.Email == email))
                {
                    var newSubscriber = new SubscriberEntity
                    {
                        Email = email
                    };

                    var created = _subscriberRepository.AddToDb(newSubscriber);

                    if(created != null)
                    {
                        return newSubscriber;
                    }
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return null!;
        }

        public async Task<IEnumerable<SubscriberEntity>> GetAllSubscribersAsync()
        {
            try
            {
                var subscribersList = await _subscriberRepository.GetAll();
                if (subscribersList.Count() > 0)
                {
                    return subscribersList;
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return null!;
        }
    }
}
