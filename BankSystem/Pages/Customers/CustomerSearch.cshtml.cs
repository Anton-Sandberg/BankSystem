using System.ComponentModel;
using System.Threading.Tasks;
using AutoMapper;
using BankSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankSystem.Pages.Customers
{
    [Authorize(Roles = "Cashier")]
    public class CustomerSearchModel : PageModel
    {
        private ICustomerService _customerService;
        private IMapper _mapper;
        public List<CustomerSearchViewModel> Customers { get; set; } = new();

        public int PageSize { get; set; } = 50;


        [BindProperty(SupportsGet = true)]
        public string SortOrder { get; set; } = string.Empty;
        [BindProperty(SupportsGet = true)]
        public string SortColumns { get; set; } = string.Empty;


        [DisplayName("First Name")]
        [BindProperty(SupportsGet = true)]
        public string GivenName { get; set; } = string.Empty;

        [DisplayName("Last Name")]
        [BindProperty(SupportsGet = true)]
        public string SurName { get; set; } = string.Empty;

        [DisplayName("City")]
        [BindProperty(SupportsGet = true)]
        public string City { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; }


        public CustomerSearchModel(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        public async Task OnGet()
        {
            try
            {
                var customerDtos = await _customerService.GetCustomersAsync(SortColumns, SortOrder, PageIndex, PageSize, GivenName, SurName, City);
                Customers = _mapper.Map<List<CustomerSearchViewModel>>(customerDtos);

            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);
            }
        }
    }
}
