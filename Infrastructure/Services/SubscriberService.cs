using Infrastructure.Dtos;
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

        public async Task<SubscriberEntity> CreateSubscriberAsync(SubscriberDto subscriber)
        {
            try
            {
                if (!await _subscriberRepository.Exists(x => x.Email == subscriber.Email))
                {
                    var newSubscriber = new SubscriberEntity
                    {
                        Email = subscriber.Email
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
                    return subscribersList;
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

        public async Task<SubscriberEntity> UpdateSubscriberAsync(int id, string newEmail)
        {
            try
            {
                if (await _subscriberRepository.Exists(s => s.Id == id) && !string.IsNullOrWhiteSpace(newEmail))
                {
                    if(!await _subscriberRepository.Exists(s => s.Email == newEmail))
                    {
                        var newSubscriberValues = new SubscriberEntity { Id = id, Email = newEmail };
                        var subscriber = await _subscriberRepository.UpdateEntity(newSubscriberValues, s => s.Id == id);
                        if (subscriber != null)
                        {
                            return subscriber;
                        }
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
