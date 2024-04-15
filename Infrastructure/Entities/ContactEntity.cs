using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities
{
    public class ContactEntity
    {
        [Key]
        public int Id { get; set; }

        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Service { get; set; } = null!;

        public string Message { get; set; } = null!;
    }
}
