using System;

namespace AcademyHub.Domain.Entities
{
    /// <summary>
    /// Represents a student in the academy.
    /// </summary>
    public class Student
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Age { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
