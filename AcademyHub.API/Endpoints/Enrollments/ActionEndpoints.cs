using AcademyHub.Application.DTOs.Requests;
using AcademyHub.Application.Interfaces;
using AcademyHub.Application.Common;
using FastEndpoints;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AcademyHub.API.Endpoints.Enrollments
{
    public class CreateEnrollmentEndpoint : Endpoint<CreateEnrollmentRequest, ApiResponse<object>>
    {
        private readonly IEnrollmentService _enrollmentService;

        public CreateEnrollmentEndpoint(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        public override void Configure()
        {
            Post("enrollments");
            AllowAnonymous();
            Summary(s => s.Summary = "Enroll a student in a class");
        }

        public override async Task HandleAsync(CreateEnrollmentRequest req, CancellationToken ct)
        {
            var result = await _enrollmentService.EnrollStudentAsync(req);
            if (result.IsSuccess)
                await Send.ResponseAsync(ApiResponse<object>.SuccessResponse(null!, "Student enrolled successfully"), result.StatusCode, ct);
            else
                await Send.ResponseAsync(ApiResponse.Error(new List<string> { result.Error! }, "Enrollment failed", result.StatusCode), result.StatusCode, ct);
        }
    }
}

namespace AcademyHub.API.Endpoints.Marks
{
    public class RecordMarkEndpoint : Endpoint<RecordMarkRequest, ApiResponse<object>>
    {
        private readonly IMarkService _markService;

        public RecordMarkEndpoint(IMarkService markService)
        {
            _markService = markService;
        }

        public override void Configure()
        {
            Post("marks");
            AllowAnonymous();
            Summary(s => s.Summary = "Record marks for a student");
        }

        public override async Task HandleAsync(RecordMarkRequest req, CancellationToken ct)
        {
            var result = await _markService.RecordMarkAsync(req);
            if (result.IsSuccess)
                await Send.ResponseAsync(ApiResponse<object>.SuccessResponse(null!, "Marks recorded successfully"), result.StatusCode, ct);
            else
                await Send.ResponseAsync(ApiResponse.Error(new List<string> { result.Error! }, "Failed to record marks", result.StatusCode), result.StatusCode, ct);
        }
    }
}
