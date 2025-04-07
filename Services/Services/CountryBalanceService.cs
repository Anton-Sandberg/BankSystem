using AutoMapper;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Services.DTOs;

namespace Services.Services
{
    internal class CountryBalanceService : ICountryBalanceService
    {
        private readonly BankAppDataContext _context;

        public CountryBalanceService(BankAppDataContext context)
        {
            _context = context;
        }

        public async Task<List<CountryBalanceDto>> GetBalancePerCountryAsync()
        {
            var result = await _context.Customers
                .SelectMany(c => c.Dispositions, (customer, disposition) => new { customer, disposition })
                .Select(x => new { x.customer.Country, x.disposition.Account.Balance })
                .GroupBy(x => x.Country)
                .Select(g => new CountryBalanceDto
                {
                    Country = g.Key,
                    TotalBalance = g.Sum(x => x.Balance)
                })
                .ToListAsync();

            return result;
        }

    }
}
