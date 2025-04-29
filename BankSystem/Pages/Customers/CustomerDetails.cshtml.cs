using System.Threading.Tasks;
using AutoMapper;
using BankSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankSystem.Pages.Customers
{
    public class CustomerDetailsModel : PageModel
    {
        private ICustomerService _customerService;
        private IMapper _mapper;

        public CustomerDetailsViewModel? ViewModel { get; set; }

        public CustomerDetailsModel(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        public async Task OnGet(int id)
        {

            try
            {
                var customerDetails = await _customerService.GetCustomerDetailsByIdAsync(id);

                ViewModel = _mapper.Map<CustomerDetailsViewModel>(customerDetails);
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }

        }
    }
}
