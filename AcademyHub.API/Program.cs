using AcademyHub.API.Middleware;
using AcademyHub.Application;
using AcademyHub.Infrastructure;
using FastEndpoints;
using FastEndpoints.Swagger;
using AcademyHub.Application.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddFastEndpoints(o =>
{
    // Enable FastEndpoints to discover and run validators that inherit FluentValidation.AbstractValidator<T>
    // and ensure discovery happens in referenced assemblies too.
    o.IncludeAbstractValidators = true;
    o.Assemblies = new[]
    {
        typeof(AcademyHub.API.Endpoints.Students.CreateStudentEndpoint).Assembly,
        typeof(CreateStudentValidator).Assembly
    };
});
builder.Services.SwaggerDocument(o =>
{
    o.DocumentSettings = s =>
    {
        s.Title = "AcademyHub API";
        s.Version = "v1";
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseFastEndpoints(c =>
{
    c.Endpoints.RoutePrefix = "api";
});

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerGen();
}

app.Run();
