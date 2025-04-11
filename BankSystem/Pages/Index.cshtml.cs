using System.Threading.Tasks;
using AutoMapper;
using BankSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Services;

namespace BankSystem.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private IMapper _mapper;

        private ICountryBalanceService _countryBalanceService;
        private ICustomerService _customerService;
        private IAccountService _accountService;

        public List<CountryBalanceViewModel> CountryBalances { get; set; } = new();

        public int CustomerCount { get; set; }
        public int AccountCount { get; set; }

        public IndexModel(ILogger<IndexModel> logger, ICountryBalanceService countryBalanceService, IMapper mapper, ICustomerService customerService, IAccountService accountService)
        {
            _logger = logger;
            _countryBalanceService = countryBalanceService;
            _mapper = mapper;
            _customerService = customerService;
            _accountService = accountService;
        }

        public async Task OnGet()
        {

            try
            {
                var countryBalances = await _countryBalanceService.GetBalancePerCountryAsync();
                CountryBalances = _mapper.Map<List<CountryBalanceViewModel>>(countryBalances);

                CustomerCount = await _customerService.GetCustomerCountAsync();
                AccountCount = await _accountService.GetAccountCountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load dashboard data");

                TempData["ErrorMessage"] = "Unable to load data. Please try again later.";

                CountryBalances = new List<CountryBalanceViewModel>();
                CustomerCount = 0;
                AccountCount = 0;
            }
        }
    }
}
