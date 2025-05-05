using System.Diagnostics;
using System.Runtime.CompilerServices;
using AutoMapper;
using DataAccess.Data;
using DataAccess.Models;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Services.DTOs;
using Services.ResultErrors;

namespace Services.Services
{
    internal class TransactionService : ITransactionService
    {
        private readonly BankAppDataContext _context;
        private readonly IMapper _mapper;

        public TransactionService(BankAppDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TransactionDto>> GetTransactionsByAccountIdAsync(int accountId, int page, int pageSize)
        {
            var transactions = await _context.Transactions
                .Where(t => t.AccountId == accountId)
                .OrderByDescending(t => t.Date)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return _mapper.Map<List<TransactionDto>>(transactions);
        }

        public async Task<Result> DepositAsync(int accountId, decimal amount)
        {
            var result = Result.Ok();

            if (amount <= 0)
            {
                result.WithError(new FieldError(nameof(TransactionDto.Amount), "Deposit amount must be greater than zero."));
            }

            var account = await _context.Accounts
                .FirstOrDefaultAsync(a => a.AccountId == accountId);

            if (account == null)
            {
                result.WithError(new FieldError(nameof(AccountDto.AccountId), $"Account with ID {accountId} not found."));
            }

            if (result.IsFailed)
            {
                return result;
            }

            account!.Balance += amount;

            _context.Transactions.Add(new Transaction
            {
                AccountId = accountId,
                Date = DateOnly.FromDateTime(DateTime.UtcNow),
                Type = "Credit",
                Operation = "Credit in Cash",
                Amount = amount,
                Balance = account.Balance,
            });

            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<Result> WithdrawAsync(int accountId, decimal amount)
        {
            var result = Result.Ok();

            if (amount <= 0)
            {
                result.WithError(new FieldError(nameof(TransactionDto.Amount), "Withdrawal amount must be greater than zero."));
            }

            var account = await _context.Accounts
                .FirstOrDefaultAsync(a => a.AccountId == accountId);

            if (account == null)
            {
                result.WithError(new FieldError(nameof(AccountDto.AccountId), $"Account with ID {accountId} not found."));
            }

            if (account != null && account.Balance < amount)
            {
                result.WithError(new FieldError(nameof(TransactionDto.Amount), "Too low balance on account."));
            }

            if (result.IsFailed)
            {
                return result;
            }

            account!.Balance -= amount;

            _context.Transactions.Add(new Transaction
            {
                AccountId = accountId,
                Date = DateOnly.FromDateTime(DateTime.UtcNow),
                Type = "Debit",
                Operation = "Withdrawal in Cash",
                Amount = -amount,
                Balance = account.Balance,
            });

            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<Result> TransferAsync(int fromAccountId, int toAccountId, decimal amount)
        {
            var result = Result.Ok();

            if (amount <= 0)
            {
                result.WithError(new FieldError(nameof(TransactionDto.Amount), "Transfer amount must be greater than zero."));
            }

            if (fromAccountId == toAccountId)
            {
                result.WithError(new FieldError(nameof(TransactionDto.Account), "Cannot transfer to the same account."));
            }

            var fromAccount = await _context.Accounts
                .FirstOrDefaultAsync(a => a.AccountId == fromAccountId);

            var toAccount = await _context.Accounts
                .FirstOrDefaultAsync(a => a.AccountId == toAccountId);

            if (fromAccount == null)
            {
                result.WithError(new FieldError(nameof(TransactionDto.AccountId), $"Source account with ID {fromAccountId} not found."));
            }

            if (toAccount == null)
            {
                result.WithError(new FieldError(nameof(TransactionDto.Account), $"Destination account with ID {toAccountId} not found."));
            }

            if (fromAccount != null && fromAccount.Balance < amount)
            {
                result.WithError(new FieldError(nameof(TransactionDto.Amount), "Too low balance on account."));
            }

            if (result.IsFailed)
            {
                return result;
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            fromAccount!.Balance -= amount;
            toAccount!.Balance += amount;

            _context.Transactions.Add(new Transaction
            {
                AccountId = fromAccountId,
                Date = DateOnly.FromDateTime(DateTime.UtcNow),
                Type = "Debit",
                Operation = "Remittance to another account",
                Amount = -amount,
                Balance = fromAccount.Balance,
                Account = toAccount.AccountId.ToString()
            });

            _context.Transactions.Add(new Transaction
            {
                AccountId = toAccountId,
                Date = DateOnly.FromDateTime(DateTime.UtcNow),
                Type = "Credit",
                Operation = "Collection from another account",
                Amount = amount,
                Balance = toAccount.Balance,
                Account = fromAccount.AccountId.ToString()
            });


            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return result.WithSuccess("Transfer completed successfully.");
        }
    }
}
