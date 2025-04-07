using AutoMapper;
using DataAccess.Data;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Services.DTOs;

namespace Services.Services
{
    internal class AccountService : IAccountService
    {
        private readonly BankAppDataContext _context;
        private readonly IMapper _mapper;

        public AccountService(BankAppDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> GetAccountCountAsync()
        {
            return await _context.Accounts.CountAsync();
        }

        public async Task<AccountDto?> GetAccountByIdAsync(int accountId)
        {
            var account = await _context.Accounts
                .FirstOrDefaultAsync(c => c.AccountId == accountId);

            return account == null ? null : _mapper.Map<AccountDto>(account);
        }
    }
}
