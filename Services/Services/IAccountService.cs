
using Services.DTOs;

namespace Services.Services
{
    public interface IAccountService
    {
        Task<AccountDto?> GetAccountByIdAsync(int accountId);
        Task<int> GetAccountCountAsync();
    }
}