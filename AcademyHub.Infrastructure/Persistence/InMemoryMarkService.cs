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
    public class InMemoryMarkService : IMarkService
    {
        private static readonly ConcurrentBag<Mark> _marks = new();

        public Task<Result> RecordMarkAsync(RecordMarkRequest request)
        {
            // Verify enrollment exists
            if (!InMemoryEnrollmentService.IsEnrolled(request.StudentId, request.ClassId))
            {
                return Task.FromResult(Result.Failure("Student is not enrolled in this class.", 400));
            }

            // In-memory update logic (remove old marks if any, or just keep latest)
            // For this task, we'll allow multiple marks but calculate average or just overwrite.
            // Let's overwrite for simplicity in recording.
            var existingMark = _marks.FirstOrDefault(m => m.StudentId == request.StudentId && m.ClassId == request.ClassId);
            if (existingMark != null)
            {
                existingMark.ExamMark = request.ExamMark;
                existingMark.AssignmentMark = request.AssignmentMark;
                existingMark.RecordedAt = DateTime.UtcNow;
            }
            else
            {
                var mark = new Mark
                {
                    Id = Guid.NewGuid(),
                    StudentId = request.StudentId,
                    ClassId = request.ClassId,
                    ExamMark = request.ExamMark,
                    AssignmentMark = request.AssignmentMark
                };
                _marks.Add(mark);
            }

            return Task.FromResult(Result.Success());
        }

        internal static List<Mark> GetByStudentId(Guid studentId) 
            => _marks.Where(m => m.StudentId == studentId).ToList();

        internal static List<Mark> GetByClassId(Guid classId) 
            => _marks.Where(m => m.ClassId == classId).ToList();

        internal static Mark? GetByEnrollment(Guid studentId, Guid classId)
            => _marks.FirstOrDefault(m => m.StudentId == studentId && m.ClassId == classId);
    }
}
