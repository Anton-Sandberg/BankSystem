using Services.DTOs;

namespace BankSystem.ViewModels
{
    public class AccountViewModel
    {
        public int AccountId { get; set; }
        public string Frequency { get; set; } = string.Empty;
        public DateOnly Created { get; set; }
        public decimal Balance { get; set; }
        public List<TransactionDto> Transactions { get; set; } = new();
    }
}
