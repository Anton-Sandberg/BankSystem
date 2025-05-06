using System.ComponentModel;
using AutoMapper;
using BankSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankSystem.Pages.Customers
{
    [Authorize(Roles = "Cashier")]
    public class CustomerByIdModel : PageModel
    {
        private ICustomerService _customerService;
        private IMapper _mapper;

        [BindProperty(SupportsGet = true)]
        public bool SearchAttempted { get; set; } = false;

        [DisplayName("Customer Id")]
        [BindProperty(SupportsGet = true)]
        public int CustomerId { get; set; }

        public CustomerSearchViewModel? CustomerSearchViewModel { get; set; }

        public CustomerByIdModel(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        public async Task OnGet()
        {
            try
            {
                var customerDto = await _customerService.GetCustomerByIdAsync(CustomerId);

                if (customerDto == null && SearchAttempted)
                {
                    ModelState.AddModelError("CustomerId", $"Customer with Id {CustomerId} not found.");
                    return;
                }

                CustomerSearchViewModel = _mapper.Map<CustomerSearchViewModel>(customerDto);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

        }
    }
}
