﻿namespace Qick.Dto.Responses
{
    public class SaveUniResponse
    {
        public Guid? UniversityId { get; set; }
        public Guid? UserId { get; set; }
        public string? AvatarUrl { get; set; }
        public string? UniName { get; set; }
        public string? Status { get; set; }
        public int Id { get; set; }
    }
}
