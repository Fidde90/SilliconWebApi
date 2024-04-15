using Infrastructure.Dtos;
using Infrastructure.Entities;

namespace Infrastructure.Factories
{
    public static class ContactFactory
    {
        public static ContactEntity ToContactEntity(ContactMessageDto dto)
        {
            if(dto != null)
            {
                var newEntity = new ContactEntity
                {
                    FullName = dto.FullName,
                    Service = dto.Service,
                    Email = dto.Email,
                    Message = dto.Message
                };

                return newEntity;
            }
            return null!;
        }
    }
}
