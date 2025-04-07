using Microsoft.EntityFrameworkCore;

namespace Services.DTOs
{
    public class CountryBalanceDto
    {
        public string Country { get; set; } = string.Empty;
        public decimal TotalBalance { get; set; }

    }
}
