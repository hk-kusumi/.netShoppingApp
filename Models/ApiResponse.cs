namespace ShoppingApplication.Models
{
    public class ApiResponse
    {
        public string Code { get; set; }
        public string Messsage { get; set; }
        public object? ResponseData { get; set; }
    }

    public enum ResponseType
    {
        Success,
        NotFound, Failure
    }
}
