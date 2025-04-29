using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Services.DTOs;

namespace BankSystem.ViewModels
{
    public class CustomerDetailsViewModel
    {
        [DisplayName("Customer Id")]
        public int CustomerId { get; set; }
        public string Gender { get; set; } = string.Empty;

        [DisplayName("First Name")]
        public string Givenname { get; set; } = string.Empty;

        [DisplayName("Last Name")]
        public string Surname { get; set; } = string.Empty;

        [DisplayName("Street address")]
        public string Streetaddress { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Zipcode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        [DisplayName("Country code")]
        public string CountryCode { get; set; } = string.Empty;

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateOnly? Birthday { get; set; }

        [DisplayName("National Id")]
        public string NationalId { get; set; } = string.Empty;

        [DisplayName("Telephone country code")]
        public string Telephonecountrycode { get; set; } = string.Empty;

        [DisplayName("Telephone number")]
        public string Telephonenumber { get; set; } = string.Empty;

        [DisplayName("Email")]
        public string Emailaddress { get; set; } = string.Empty;
        public List<AccountDto> Accounts { get; set; } = new();

        [DisplayName("Total balance")]
        public decimal TotalBalance { get; set; }
    }
}
