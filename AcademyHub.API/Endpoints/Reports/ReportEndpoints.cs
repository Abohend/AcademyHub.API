using AcademyHub.Application.DTOs.Responses;
using AcademyHub.Application.Interfaces;
using AcademyHub.Application.Common;
using FastEndpoints;

namespace AcademyHub.API.Endpoints.Reports
{
    public class GetStudentReportEndpoint : EndpointWithoutRequest<ApiResponse<StudentReportResponse>>
    {
        private readonly IStudentService _studentService;

        public GetStudentReportEndpoint(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public override void Configure()
        {
            Get("students/{id:guid}/report");
            AllowAnonymous();
            Summary(s => s.Summary = "Generate student report");
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var id = Route<Guid>("id");
            var result = await _studentService.GetStudentReportAsync(id);

            if (result.IsSuccess)
                await Send.ResponseAsync(ApiResponse<StudentReportResponse>.SuccessResponse(result.Value!), result.StatusCode, ct);
            else
                await Send.ResponseAsync(ApiResponse<StudentReportResponse>.ErrorResponse(new List<string> { result.Error! }, "Report failed", result.StatusCode), result.StatusCode, ct);
        }
    }

    public class GetClassAverageEndpoint : EndpointWithoutRequest<ApiResponse<AverageMarksResponse>>
    {
        private readonly IClassService _classService;

        public GetClassAverageEndpoint(IClassService classService)
        {
            _classService = classService;
        }

        public override void Configure()
        {
            Get("classes/{id:guid}/average-marks");
            AllowAnonymous();
            Summary(s => s.Summary = "Calculate average marks for a class");
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var id = Route<Guid>("id");
            var result = await _classService.GetAverageMarksAsync(id);

            if (result.IsSuccess)
                await Send.ResponseAsync(ApiResponse<AverageMarksResponse>.SuccessResponse(result.Value!), result.StatusCode, ct);
            else
                await Send.ResponseAsync(ApiResponse<AverageMarksResponse>.ErrorResponse(new List<string> { result.Error! }, "Calculation failed", result.StatusCode), result.StatusCode, ct);
        }
    }

}
