using System;
using System.Collections.Generic;

namespace AcademyHub.Application.DTOs.Responses
{
    public class AverageMarksResponse
    {
        public string ClassName { get; set; } = string.Empty;
        public double AverageTotalMark { get; set; }
        public int StudentCount { get; set; }
    }

    public class StudentReportResponse
    {
        public Guid StudentId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public List<ClassReportItem> EnrolledClasses { get; set; } = new();
        public double OverallAverageMark { get; set; }
    }

    public class ClassReportItem
    {
        public string ClassName { get; set; } = string.Empty;
        public double ExamMark { get; set; }
        public double AssignmentMark { get; set; }
        public double TotalMark { get; set; }
    }
}
