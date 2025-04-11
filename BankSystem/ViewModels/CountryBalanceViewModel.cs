namespace BankSystem.ViewModels
{
    public class CountryBalanceViewModel
    {
        public string CountryCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public decimal TotalBalance { get; set; }
    }
}
