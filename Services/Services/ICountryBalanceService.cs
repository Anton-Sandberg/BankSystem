using Services.DTOs;

namespace Services.Services
{
    public interface ICountryBalanceService
    {
        Task<List<CountryBalanceDto>> GetBalancePerCountryAsync();
    }
}