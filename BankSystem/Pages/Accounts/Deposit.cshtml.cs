using AutoMapper;
using BankSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.ResultErrors;
using Services.Services;

namespace BankSystem.Pages.Accounts
{
    public class DepositModel : PageModel
    {
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;

        [BindProperty(SupportsGet = true)]
        public int AccountId { get; set; }

        [BindProperty]
        public decimal Amount { get; set; }

        public AccountViewModel? AccountViewModel { get; set; }

        public DepositModel(IMapper mapper, IAccountService accountService, ITransactionService transactionService)
        {
            _mapper = mapper;
            _accountService = accountService;
            _transactionService = transactionService;
        }

        public async Task OnGetAsync(int accountId)
        {
            var dto = await _accountService.GetAccountByIdAsync(accountId);
            AccountViewModel = _mapper.Map<AccountViewModel>(dto);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var result = await _transactionService.DepositAsync(AccountId, Amount);

            if (result.IsFailed)
            {
                foreach (var error in result.Errors)
                {
                    if (error is FieldError fieldError)
                    {
                        ModelState.AddModelError(fieldError.Field, fieldError.Message);

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, error.Message);
                    }
                }

                var dto = await _accountService.GetAccountByIdAsync(AccountId);
                AccountViewModel = _mapper.Map<AccountViewModel>(dto);

                return Page();
            }

            TempData["SuccessMessage"] = "Deposit completed successfully!";
            return RedirectToPage("/Accounts/AccountDetails", new { accountId = AccountId });
        }
    }
}

