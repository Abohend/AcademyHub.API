using System;

namespace AcademyHub.Application.DTOs.Requests
{
    public class GetTopStudentRequest
    {
        public Guid? ClassId { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
