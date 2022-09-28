using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class User
    {
        public User()
        {
            Attempts = new HashSet<Attempt>();
            Fqas = new HashSet<Fqa>();
            Tests = new HashSet<Test>();
        }

        public Guid Id { get; set; }
        public Guid? UniversityId { get; set; }
        public string? UserName { get; set; }
        public string? RoleId { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Age { get; set; }
        public string? Phone { get; set; }
        public DateTime? SignUpDate { get; set; }
        public string? CredentialId { get; set; }
        public string? AvatarUrl { get; set; }
        public string? Email { get; set; }
        public string? Status { get; set; }
        public string? AddressNumber { get; set; }
        public int? WardId { get; set; }

        public virtual UserRole? Role { get; set; }
        public virtual University? University { get; set; }
        public virtual Ward? Ward { get; set; }
        public virtual ICollection<Attempt> Attempts { get; set; }
        public virtual ICollection<Fqa> Fqas { get; set; }
        public virtual ICollection<Test> Tests { get; set; }
    }
}
