using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademyHub.Application.Common;
using AcademyHub.Application.DTOs.Requests;
using AcademyHub.Application.Interfaces;
using AcademyHub.Domain.Entities;

namespace AcademyHub.Infrastructure.Persistence
{
    public class InMemoryEnrollmentService : IEnrollmentService
    {
        private static readonly ConcurrentBag<Enrollment> _enrollments = new();

        public Task<Result> EnrollStudentAsync(CreateEnrollmentRequest request)
        {
            // Verify student exists
            if (InMemoryStudentService.GetById(request.StudentId) == null)
            {
                return Task.FromResult(Result.Failure("Student not found.", 404));
            }

            // Verify class exists
            if (InMemoryClassService.GetById(request.ClassId) == null)
            {
                return Task.FromResult(Result.Failure("Class not found.", 404));
            }

            // Prevent duplicate enrollment
            if (_enrollments.Any(e => e.StudentId == request.StudentId && e.ClassId == request.ClassId))
            {
                return Task.FromResult(Result.Failure("Student is already enrolled in this class.", 409));
            }

            var enrollment = new Enrollment
            {
                Id = Guid.NewGuid(),
                StudentId = request.StudentId,
                ClassId = request.ClassId
            };

            _enrollments.Add(enrollment);
            return Task.FromResult(Result.Success());
        }

        internal static List<Enrollment> GetByStudentId(Guid studentId) 
            => _enrollments.Where(e => e.StudentId == studentId).ToList();

        internal static List<Enrollment> GetByClassId(Guid classId) 
            => _enrollments.Where(e => e.ClassId == classId).ToList();

        internal static bool IsEnrolled(Guid studentId, Guid classId)
            => _enrollments.Any(e => e.StudentId == studentId && e.ClassId == classId);
    }
}
