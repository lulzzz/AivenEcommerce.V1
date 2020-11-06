
using System;

using AivenEcommerce.V1.Domain.Enums;

namespace AivenEcommerce.V1.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public bool VerifiedEmail { get; set; }
        public UserRole Role { get; set; }
        public Guid? AmbassadorId { get; set; }

    }
}
