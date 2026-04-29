using AcademyHub.API.Middleware;
using FastEndpoints;
using FastEndpoints.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddFastEndpoints();
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
