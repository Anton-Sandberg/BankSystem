using System.Drawing.Printing;
using System.Threading.Tasks;
using AutoMapper;
using BankSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Services;

namespace BankSystem.Pages.Accounts
{
    public class AccountDetailsModel : PageModel
    {
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;

        public readonly int PageSize = 20;

        [BindProperty(SupportsGet = true)]
        public AccountViewModel? AccountViewModel { get; set; }

        public List<TransactionViewModel> TransactionViewModels { get; set; } = new();

        public AccountDetailsModel(IAccountService accountService, IMapper mapper, ITransactionService transactionService)
        {
            _accountService = accountService;
            _mapper = mapper;
            _transactionService = transactionService;
        }


        public async Task OnGet(int accountId)
        {
            try
            {
                var accountDto = await _accountService.GetAccountByIdAsync(accountId);

                var transactionsDtos = await _transactionService.GetTransactionsByAccountIdAsync(accountId, 1, PageSize);

                TransactionViewModels = _mapper.Map<List<TransactionViewModel>>(transactionsDtos);

                AccountViewModel = _mapper.Map<AccountViewModel>(accountDto);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
        }

        public async Task<IActionResult> OnGetLoadMoreAsync(int accountId, int currentPage)
        {
            try
            {
                var transactionDtos = await _transactionService.GetTransactionsByAccountIdAsync(accountId, currentPage, PageSize);

                var transactionViewModels = _mapper.Map<List<TransactionViewModel>>(transactionDtos);

                return new JsonResult(transactionViewModels);
            }
            catch (Exception ex)
            {
                return new JsonResult(new { error = ex.Message });
            }
        }
    }
}
