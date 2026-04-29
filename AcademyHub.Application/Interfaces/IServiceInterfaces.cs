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
    }
}
