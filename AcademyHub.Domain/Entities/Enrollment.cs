using System;

namespace AcademyHub.Domain.Entities
{
    /// <summary>
    /// Represents a student enrollment in a class.
    /// </summary>
    public class Enrollment
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid ClassId { get; set; }
        public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
    }
}
