namespace AcademyHub.Application.DTOs.Requests
{
    public class GetAllClassRequest
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public string? name {  get; set; }
        public string? teacher { get; set; }
    }
}

