using FluentResults;
using Services.DTOs;

namespace Services.Services
{
    public interface ITransactionService
    {
        Task<Result> DepositAsync(int accountId, decimal amount);
        Task<List<TransactionDto>> GetTransactionsByAccountIdAsync(int accountId, int page, int pageSize);
        Task<Result> TransferAsync(int fromAccountId, int toAccountId, decimal amount);
        Task<Result> WithdrawAsync(int accountId, decimal amount);
    }
}