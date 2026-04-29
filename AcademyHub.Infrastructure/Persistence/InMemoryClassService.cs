using System.Collections.Concurrent;
using AcademyHub.Application.Common;
using AcademyHub.Application.DTOs.Requests;
using AcademyHub.Application.DTOs.Responses;
using AcademyHub.Application.Interfaces;
using AcademyHub.Domain.Entities;

namespace AcademyHub.Infrastructure.Persistence
{
    public class InMemoryClassService : IClassService
    {
        private static readonly ConcurrentDictionary<Guid, Class> _classes = new();

        public Task<Result<ClassResponse>> CreateClassAsync(CreateClassRequest request)
        {
            var @class = new Class
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Teacher = request.Teacher,
                Description = request.Description
            };

            if (_classes.TryAdd(@class.Id, @class))
            {
                return Task.FromResult(Result<ClassResponse>.Success(MapToResponse(@class)));
            }

            return Task.FromResult(Result<ClassResponse>.Failure("Failed to create class."));
        }

        public Task<Result<List<ClassResponse>>> GetAllClassesAsync(int page, int pageSize, string? name, string? teacher)
        {
            var query = _classes.Values.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(c => c.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(teacher))
            {
                query = query.Where(c => c.Teacher.Contains(teacher, StringComparison.OrdinalIgnoreCase));
            }

            var classes = query
                .OrderBy(c => c.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(MapToResponse)
                .ToList();

            return Task.FromResult(Result<List<ClassResponse>>.Success(classes));
        }

        public Task<Result> DeleteClassAsync(Guid id)
        {
            if (_classes.TryRemove(id, out _))
            {
                return Task.FromResult(Result.Success());
            }

            return Task.FromResult(Result.Failure("Class not found.", 404));
        }

        public Task<Result<AverageMarksResponse>> GetAverageMarksAsync(Guid classId)
        {
            var @class = GetById(classId);
            if (@class == null)
            {
                return Task.FromResult(Result<AverageMarksResponse>.Failure("Class not found.", 404));
            }

            var marks = InMemoryMarkService.GetByClassId(classId);
            if (!marks.Any())
            {
                return Task.FromResult(Result<AverageMarksResponse>.Failure("No marks recorded for this class."));
            }

            var response = new AverageMarksResponse
            {
                ClassName = @class.Name,
                AverageTotalMark = marks.Average(m => m.TotalMark),
                StudentCount = marks.Select(m => m.StudentId).Distinct().Count()
            };

            return Task.FromResult(Result<AverageMarksResponse>.Success(response));
        }

        public Task<Result<List<TopStudentResponse>>> GetTopStudentsAsync(GetTopStudentRequest req)
        {
            var classId = req.ClassId;
            var classes = new List<Class>();
            if (classId != null)
            {
                // check class existance
                if (GetById(classId.Value) == null)
                {
                    return Task.FromResult(Result<List<TopStudentResponse>>.Failure("Class not found.", 404));
                }
                classes = _classes.Values.Where(c => c.Id == classId.Value).ToList();
            }
            else
            {
                classes = _classes.Values.ToList();
            }

            var response = new List<TopStudentResponse>();

            foreach (var c in classes)
            {
                var topMark = InMemoryMarkService.GetByClassId(c.Id)
                    .OrderByDescending(m => m.TotalMark)
                    .FirstOrDefault();

                var topStudent = topMark == null ? null : InMemoryStudentService.GetById(topMark.StudentId);

                response.Add(new TopStudentResponse
                {
                    ClassId = c.Id,
                    ClassName = c.Name,
                    StudentId = topMark != null ? topMark.StudentId : Guid.Empty,
                    StudentName = topStudent != null ? topStudent.FirstName + " " + topStudent.LastName : "No students",
                    TotalMark = topMark != null ? topMark.TotalMark : 0
                });

            }
            return Task.FromResult(Result<List<TopStudentResponse>>.Success(response));
        }

        private static ClassResponse MapToResponse(Class @class)
        {
            return new ClassResponse
            {
                Id = @class.Id,
                Name = @class.Name,
                Teacher = @class.Teacher,
                Description = @class.Description,
                CreatedAt = @class.CreatedAt
            };
        }

        internal static Class? GetById(Guid id) => _classes.TryGetValue(id, out var c) ? c : null;
    }
}
