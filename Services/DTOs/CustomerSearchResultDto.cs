namespace Services.DTOs
{
    public class CustomerSearchResultDto
    {
        public int CustomerId { get; set; }
        public string NationalId { get; set; } = string.Empty;
        public string Givenname { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Streetaddress { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
    }
}
