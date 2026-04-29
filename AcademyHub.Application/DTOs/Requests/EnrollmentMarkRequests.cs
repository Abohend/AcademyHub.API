using System;

namespace AcademyHub.Application.DTOs.Requests
{
    public class CreateEnrollmentRequest
    {
        public Guid StudentId { get; set; }
        public Guid ClassId { get; set; }
    }

    public class RecordMarkRequest
    {
        public Guid StudentId { get; set; }
        public Guid ClassId { get; set; }
        public double ExamMark { get; set; }
        public double AssignmentMark { get; set; }
    }
}
