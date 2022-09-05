namespace ECommerceCoreDapper.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string TCKNError { get; set; } = "Hatalý TC Kimlik Numarasý";
    }
}