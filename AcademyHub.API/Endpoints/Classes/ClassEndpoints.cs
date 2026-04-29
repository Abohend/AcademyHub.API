using AcademyHub.Application.DTOs.Requests;
using AcademyHub.Application.DTOs.Responses;
using AcademyHub.Application.Interfaces;
using AcademyHub.Application.Common;
using FastEndpoints;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AcademyHub.API.Endpoints.Classes
{
    public class CreateClassEndpoint : Endpoint<CreateClassRequest, ApiResponse<ClassResponse>>
    {
        private readonly IClassService _classService;

        public CreateClassEndpoint(IClassService classService)
        {
            _classService = classService;
        }

        public override void Configure()
        {
            Post("classes");
            AllowAnonymous();
            Summary(s => s.Summary = "Create a new class");
        }

        public override async Task HandleAsync(CreateClassRequest req, CancellationToken ct)
        {
            var result = await _classService.CreateClassAsync(req);
            if (result.IsSuccess)
                await Send.ResponseAsync(ApiResponse<ClassResponse>.SuccessResponse(result.Value!), result.StatusCode, ct);
            else
                await Send.ResponseAsync(ApiResponse<ClassResponse>.ErrorResponse(new List<string> { result.Error! }, "Failed to create class", result.StatusCode), result.StatusCode, ct);
        }
    }

    public class GetAllClassesEndpoint : EndpointWithoutRequest<ApiResponse<List<ClassResponse>>>
    {
        private readonly IClassService _classService;

        public GetAllClassesEndpoint(IClassService classService)
        {
            _classService = classService;
        }

        public override void Configure()
        {
            Get("classes");
            AllowAnonymous();
            Summary(s => s.Summary = "Get all classes");
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var page = Query<int>("page", isRequired: false);
            var pageSize = Query<int>("pageSize", isRequired: false);
            var name = Query<string>("name", isRequired: false);
            var teacher = Query<string>("teacher", isRequired: false);

            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            var result = await _classService.GetAllClassesAsync(page, pageSize, name, teacher);
            await Send.ResponseAsync(ApiResponse<List<ClassResponse>>.SuccessResponse(result.Value!), result.StatusCode, ct);
        }
    }

    public class DeleteClassEndpoint : EndpointWithoutRequest<ApiResponse<object>>
    {
        private readonly IClassService _classService;

        public DeleteClassEndpoint(IClassService classService)
        {
            _classService = classService;
        }

        public override void Configure()
        {
            Delete("classes/{id:guid}");
            AllowAnonymous();
            Summary(s => s.Summary = "Delete a class");
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var id = Route<Guid>("id");
            var result = await _classService.DeleteClassAsync(id);
            if (result.IsSuccess)
                await Send.ResponseAsync(ApiResponse<object>.SuccessResponse(null!, "Class deleted"), result.StatusCode, ct);
            else
                await Send.ResponseAsync(ApiResponse.Error(new List<string> { result.Error! }, "Delete failed", result.StatusCode), result.StatusCode, ct);
        }
    }
}
