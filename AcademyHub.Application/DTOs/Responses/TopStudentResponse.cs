using System;

namespace AcademyHub.Application.DTOs.Responses
{
    public class TopStudentResponse
    {
        public Guid ClassId { get; set; }
        public string ClassName { get; set; } = string.Empty;
        public Guid StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public double TotalMark { get; set; }
    }
}
