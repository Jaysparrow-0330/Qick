﻿using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class User
    {
        public User()
        {
            AcademicProfiles = new HashSet<AcademicProfile>();
            Applications = new HashSet<Application>();
            Attempts = new HashSet<Attempt>();
            Fqas = new HashSet<Fqa>();
            MailBoxes = new HashSet<MailBox>();
            SavedUnis = new HashSet<SavedUni>();
            Tests = new HashSet<Test>();
            UserOtps = new HashSet<UserOtp>();
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
        public string? CredentialFrontImgUrl { get; set; }
        public string? CredentialBackImgUrl { get; set; }
        public Guid? HighSchoolId { get; set; }
        public int? WardId { get; set; }
        public string? PublicProfile { get; set; }

        public virtual HighSchool? HighSchool { get; set; }
        public virtual UserRole? Role { get; set; }
        public virtual University? University { get; set; }
        public virtual Ward? Ward { get; set; }
        public virtual ICollection<AcademicProfile> AcademicProfiles { get; set; }
        public virtual ICollection<Application> Applications { get; set; }
        public virtual ICollection<Attempt> Attempts { get; set; }
        public virtual ICollection<Fqa> Fqas { get; set; }
        public virtual ICollection<MailBox> MailBoxes { get; set; }
        public virtual ICollection<SavedUni> SavedUnis { get; set; }
        public virtual ICollection<Test> Tests { get; set; }
        public virtual ICollection<UserOtp> UserOtps { get; set; }
    }
}
