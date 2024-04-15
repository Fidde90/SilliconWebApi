using Infrastructure.Dtos;
using Infrastructure.Factories;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Infrastructure.Services
{
    public class ContactService(ContactRepository contactRepository)
    {
        private readonly ContactRepository _contactRepository = contactRepository;

        public async Task<bool> CreateMessageAsync(ContactMessageDto dto)
        {
            try
            {
                if (dto != null)
                {
                    var newMessageEntity = ContactFactory.ToContactEntity(dto);

                    var result = await _contactRepository.AddToDb(newMessageEntity);
                    if (result != null)
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
