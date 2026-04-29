using AcademyHub.Application.Common;
using AcademyHub.Application.DTOs.Requests;
using AcademyHub.Application.DTOs.Responses;
using AcademyHub.Application.Interfaces;
using FastEndpoints;

namespace AcademyHub.API.Endpoints.Students
{
    public class GetTopStudentsEndpoint : Endpoint<GetTopStudentRequest, ApiResponse<List<TopStudentResponse>>>
    {
        private readonly IClassService _classService;

        public GetTopStudentsEndpoint(IClassService classService)
        {
            _classService = classService;
        }

        public override void Configure()
        {
            Get("classes/{ClassId}/top-students");
            AllowAnonymous();
            Summary(s => {
                s.Summary = "Get top students in a class";
                s.Description = "Retrieves the top students based on their average marks for a specific class.";
            });
        }

        public override async Task HandleAsync(GetTopStudentRequest req, CancellationToken ct)
        {
            var result = await _classService.GetTopStudentsAsync(req);

            if (result.IsSuccess)
            {
                await Send.ResponseAsync(ApiResponse<List<TopStudentResponse>>.SuccessResponse(result.Value!), result.StatusCode, ct);
            }
            else
            {
                await Send.ResponseAsync(ApiResponse<List<TopStudentResponse>>.ErrorResponse(new List<string> { result.Error! }, "Failed to retrieve top students", result.StatusCode), result.StatusCode, ct);
            }
        }
    }
}
