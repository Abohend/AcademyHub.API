using AcademyHub.Application.Common;
using AcademyHub.Application.DTOs.Requests;
using AcademyHub.Application.DTOs.Responses;

namespace AcademyHub.Application.Interfaces
{
    public interface IStudentService
    {
        Task<Result<StudentResponse>> CreateStudentAsync(CreateStudentRequest request);
        Task<Result<List<StudentResponse>>> GetAllStudentsAsync(int page, int pageSize, string? name, int? age);
        Task<Result<StudentResponse>> UpdateStudentAsync(Guid id, UpdateStudentRequest request);
        Task<Result> DeleteStudentAsync(Guid id);
        Task<Result<StudentReportResponse>> GetStudentReportAsync(Guid studentId);
    }

    public interface IClassService
    {
        Task<Result<ClassResponse>> CreateClassAsync(CreateClassRequest request);
        Task<Result<List<ClassResponse>>> GetAllClassesAsync(int page, int pageSize, string? name, string? teacher);
        Task<Result> DeleteClassAsync(Guid id);
        Task<Result<AverageMarksResponse>> GetAverageMarksAsync(Guid classId);
        Task<Result<List<TopStudentResponse>>> GetTopStudentsAsync(GetTopStudentRequest request);
    }

    public interface IEnrollmentService
    {
        Task<Result> EnrollStudentAsync(CreateEnrollmentRequest request);
    }

    public interface IMarkService
    {
        Task<Result> RecordMarkAsync(RecordMarkRequest request);
    }
}
