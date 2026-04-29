namespace AcademyHub.Application.DTOs.Requests
{
    public class GetAllClassRequest
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Name {  get; set; }
        public string? Teacher { get; set; }
    }
}

