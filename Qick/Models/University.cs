﻿using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class University
    {
        public University()
        {
            AddmissionNews = new HashSet<AddmissionNews>();
            Applications = new HashSet<Application>();
            Fqas = new HashSet<Fqa>();
            MailBoxes = new HashSet<MailBox>();
            SavedUnis = new HashSet<SavedUni>();
            UniversitySpecializations = new HashSet<UniversitySpecialization>();
            Users = new HashSet<User>();
        }

        public Guid Id { get; set; }
        public string? UniName { get; set; }
        public string? AddressNumber { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? WebsiteUrl { get; set; }
        public string? AvatarUrl { get; set; }
        public string? CoverPhotoUrl { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? Vippack { get; set; }
        public string? Status { get; set; }
        public string? UniCode { get; set; }
        public string? Description { get; set; }
        public int? CountSaved { get; set; }
        public int? WardId { get; set; }

        public virtual Ward? Ward { get; set; }
        public virtual ICollection<AddmissionNews> AddmissionNews { get; set; }
        public virtual ICollection<Application> Applications { get; set; }
        public virtual ICollection<Fqa> Fqas { get; set; }
        public virtual ICollection<MailBox> MailBoxes { get; set; }
        public virtual ICollection<SavedUni> SavedUnis { get; set; }
        public virtual ICollection<UniversitySpecialization> UniversitySpecializations { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
