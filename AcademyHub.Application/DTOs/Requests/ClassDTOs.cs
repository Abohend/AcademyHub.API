using System;

namespace AcademyHub.Application.DTOs.Requests
{
    public class CreateClassRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Teacher { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}

namespace AcademyHub.Application.DTOs.Responses
{
    public class ClassResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Teacher { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
