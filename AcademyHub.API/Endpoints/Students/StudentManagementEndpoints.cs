using AcademyHub.Application.DTOs.Requests;
using AcademyHub.Application.DTOs.Responses;
using AcademyHub.Application.Interfaces;
using AcademyHub.Application.Common;
using FastEndpoints;

namespace AcademyHub.API.Endpoints.Students
{
    public class UpdateStudentEndpoint : Endpoint<UpdateStudentRequest, ApiResponse<StudentResponse>>
    {
        private readonly IStudentService _studentService;

        public UpdateStudentEndpoint(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public override void Configure()
        {
            Put("students/{id:guid}");
            AllowAnonymous();
            Summary(s => s.Summary = "Update a student");
        }

        public override async Task HandleAsync(UpdateStudentRequest req, CancellationToken ct)
        {
            var id = Route<Guid>("id");
            var result = await _studentService.UpdateStudentAsync(id, req);

            if (result.IsSuccess)
            {
                await Send.ResponseAsync(ApiResponse<StudentResponse>.SuccessResponse(result.Value!), result.StatusCode, ct);
            }
            else
            {
                await Send.ResponseAsync(ApiResponse<StudentResponse>.ErrorResponse(new List<string> { result.Error! }, "Update failed", result.StatusCode), result.StatusCode, ct);
            }
        }
    }

    public class DeleteStudentEndpoint : EndpointWithoutRequest<ApiResponse<object>>
    {
        private readonly IStudentService _studentService;

        public DeleteStudentEndpoint(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public override void Configure()
        {
            Delete("students/{id:guid}");
            AllowAnonymous();
            Summary(s => s.Summary = "Delete a student");
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var id = Route<Guid>("id");
            var result = await _studentService.DeleteStudentAsync(id);

            if (result.IsSuccess)
            {
                await Send.ResponseAsync(ApiResponse<object>.SuccessResponse(null!, "Student deleted successfully"), result.StatusCode, ct);
            }
            else
            {
                await Send.ResponseAsync(ApiResponse.Error(new List<string> { result.Error! }, "Delete failed", result.StatusCode), result.StatusCode, ct);
            }
        }
    }
}
