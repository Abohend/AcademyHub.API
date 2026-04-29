using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademyHub.Application.Common;
using AcademyHub.Application.DTOs.Requests;
using AcademyHub.Application.DTOs.Responses;
using AcademyHub.Application.Interfaces;
using AcademyHub.Domain.Entities;

namespace AcademyHub.Infrastructure.Persistence
{
    public class InMemoryStudentService : IStudentService
    {
        private static readonly ConcurrentDictionary<Guid, Student> _students = new();
        private readonly IEnrollmentService _enrollmentService;
        private readonly IMarkService _markService;

        public InMemoryStudentService(IEnrollmentService enrollmentService, IMarkService markService)
        {
            _enrollmentService = enrollmentService;
            _markService = markService;
        }

        public Task<Result<StudentResponse>> CreateStudentAsync(CreateStudentRequest request)
        {
            var student = new Student
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Age = request.Age
            };

            if (_students.TryAdd(student.Id, student))
            {
                return Task.FromResult(Result<StudentResponse>.Success(MapToResponse(student)));
            }

            return Task.FromResult(Result<StudentResponse>.Failure("Failed to create student."));
        }

        public Task<Result<List<StudentResponse>>> GetAllStudentsAsync(int page, int pageSize, string? name, int? age)
        {
            var query = _students.Values.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(s => s.FirstName.Contains(name, StringComparison.OrdinalIgnoreCase) || 
                                         s.LastName.Contains(name, StringComparison.OrdinalIgnoreCase));
            }

            if (age.HasValue)
            {
                query = query.Where(s => s.Age == age.Value);
            }

            var students = query
                .OrderBy(s => s.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(MapToResponse)
                .ToList();

            return Task.FromResult(Result<List<StudentResponse>>.Success(students));
        }

        public Task<Result<StudentResponse>> UpdateStudentAsync(Guid id, UpdateStudentRequest request)
        {
            if (!_students.TryGetValue(id, out var student))
            {
                return Task.FromResult(Result<StudentResponse>.Failure("Student not found.", 404));
            }

            student.FirstName = request.FirstName;
            student.LastName = request.LastName;
            student.Age = request.Age;

            return Task.FromResult(Result<StudentResponse>.Success(MapToResponse(student)));
        }

        public Task<Result> DeleteStudentAsync(Guid id)
        {
            if (_students.TryRemove(id, out _))
            {
                return Task.FromResult(Result.Success());
            }

            return Task.FromResult(Result.Failure("Student not found.", 404));
        }

        public Task<Result<StudentReportResponse>> GetStudentReportAsync(Guid studentId)
        {
            var student = GetById(studentId);
            if (student == null)
            {
                return Task.FromResult(Result<StudentReportResponse>.Failure("Student not found.", 404));
            }

            var enrollments = InMemoryEnrollmentService.GetByStudentId(studentId);
            var marks = InMemoryMarkService.GetByStudentId(studentId);

            var reportItems = enrollments.Select(e => {
                var @class = InMemoryClassService.GetById(e.ClassId);
                var mark = marks.FirstOrDefault(m => m.ClassId == e.ClassId);
                
                return new ClassReportItem
                {
                    ClassName = @class?.Name ?? "Unknown Class",
                    ExamMark = mark?.ExamMark ?? 0,
                    AssignmentMark = mark?.AssignmentMark ?? 0,
                    TotalMark = mark?.TotalMark ?? 0
                };
            }).ToList();

            var overallAverage = reportItems.Any() ? reportItems.Average(r => r.TotalMark) : 0;

            var response = new StudentReportResponse
            {
                StudentId = student.Id,
                FullName = $"{student.FirstName} {student.LastName}",
                EnrolledClasses = reportItems,
                OverallAverageMark = overallAverage
            };

            return Task.FromResult(Result<StudentReportResponse>.Success(response));
        }

        private static StudentResponse MapToResponse(Student student)
        {
            return new StudentResponse
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Age = student.Age,
                CreatedAt = student.CreatedAt
            };
        }
        
        // Helper for the report
        internal static Student? GetById(Guid id) => _students.TryGetValue(id, out var s) ? s : null;
    }
}
