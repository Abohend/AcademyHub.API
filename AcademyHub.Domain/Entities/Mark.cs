using System;

namespace AcademyHub.Domain.Entities
{
    /// <summary>
    /// Represents marks recorded for a student in a class.
    /// </summary>
    public class Mark
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid ClassId { get; set; }
        public double ExamMark { get; set; }
        public double AssignmentMark { get; set; }

        /// <summary>
        /// Total mark is the sum of exam and assignment marks.
        /// </summary>
        public double TotalMark => ExamMark + AssignmentMark;
        
        public DateTime RecordedAt { get; set; } = DateTime.UtcNow;
    }
}
