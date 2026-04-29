using AcademyHub.Application.DTOs.Requests;
using AcademyHub.Application.DTOs.Responses;
using AcademyHub.Application.Interfaces;
using AcademyHub.Application.Common;
using FastEndpoints;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AcademyHub.API.Endpoints.Students
{
    public class CreateStudentEndpoint : Endpoint<CreateStudentRequest, ApiResponse<StudentResponse>>
    {
        private readonly IStudentService _studentService;

        public CreateStudentEndpoint(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public override void Configure()
        {
            Post("students");
            AllowAnonymous();
            Summary(s => {
                s.Summary = "Create a new student";
                s.Description = "Adds a new student to the in-memory collection.";
            });
        }

        public override async Task HandleAsync(CreateStudentRequest req, CancellationToken ct)
        {
            var result = await _studentService.CreateStudentAsync(req);
            
            if (result.IsSuccess)
            {
                await Send.ResponseAsync(ApiResponse<StudentResponse>.SuccessResponse(result.Value!), result.StatusCode, ct);
            }
            else
            {
                await Send.ResponseAsync(ApiResponse<StudentResponse>.ErrorResponse(new List<string> { result.Error! }, "Failed to create student", result.StatusCode), result.StatusCode, ct);
            }
        }
    }

    public class GetAllStudentsEndpoint : EndpointWithoutRequest<PagedResponse<List<StudentResponse>>>
    {
        private readonly IStudentService _studentService;

        public GetAllStudentsEndpoint(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public override void Configure()
        {
            Get("students");
            AllowAnonymous();
            Summary(s => {
                s.Summary = "Get all students";
                s.Description = "Retrieves a paginated list of students with optional filtering.";
            });
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var page = Query<int>("page", isRequired: false);
            var pageSize = Query<int>("pageSize", isRequired: false);
            var name = Query<string>("name", isRequired: false);
            var age = Query<int?>("age", isRequired: false);

            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            var result = await _studentService.GetAllStudentsAsync(page, pageSize, name, age);
            
            if (result.IsSuccess)
            {
                await Send.ResponseAsync(PagedResponse<List<StudentResponse>>.Create(result.Value!, result.TotalCount, result.PageNumber, result.PageSize), result.StatusCode, ct);
            }
            else
            {
                await Send.ResponseAsync(PagedResponse<List<StudentResponse>>.ErrorResponse(new List<string> { result.Error! }, "Failed to retrieve students", result.StatusCode), result.StatusCode, ct);
            }
        }
    }
}
