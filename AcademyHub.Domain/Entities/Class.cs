using System;

namespace AcademyHub.Domain.Entities
{
    /// <summary>
    /// Represents a class in the academy.
    /// </summary>
    public class Class
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Teacher { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
