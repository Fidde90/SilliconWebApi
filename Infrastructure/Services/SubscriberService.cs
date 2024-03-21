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

                    var created = await _subscriberRepository.AddToDb(newSubscriber);

                    if (created != null)
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

        public async Task<SubscriberEntity> GetOneSubscriberAsync(int id)
        {
            try
            {
                var subscriber = await _subscriberRepository.GetOne(s => s.Id == id);
                if (subscriber != null)
                {
                    return subscriber;
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return null!;
        }

        public async Task<SubscriberEntity> UpdateSubscriberAsync(int id, string email)
        {
            try
            {
                if (await _subscriberRepository.Exists(s => s.Id == id) && !string.IsNullOrWhiteSpace(email))
                {
                    var newSubscriberValues = new SubscriberEntity { Id = id, Email = email };
                    var subscriber = await _subscriberRepository.UpdateEntity(newSubscriberValues, s => s.Id == id);
                    if (subscriber != null)
                    {
                        return subscriber;
                    }
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return null!;
        }

        public async Task<bool> DeleteOneSubscriber(int id)
        {
            try
            {
                if (await _subscriberRepository.Exists(s => s.Id == id))
                {
                    var result = await _subscriberRepository.DeleteFromDb(s => s.Id == id);
                    if (result == true)
                    {
                        return true;
                    }
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return false;
        }
    }
}
