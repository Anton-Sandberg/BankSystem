﻿namespace Services.DTOs
{
    public class CustomerDetailsDto
    {
        public int CustomerId { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string Givenname { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Streetaddress { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Zipcode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty; 
        public string CountryCode { get; set; } = string.Empty;
        public DateOnly? Birthday { get; set; }
        public string NationalId { get; set; } = string.Empty;
        public string Telephonecountrycode { get; set; } = string.Empty;
        public string Telephonenumber { get; set; } = string.Empty;
        public string Emailaddress { get; set; } = string.Empty;
        public List<AccountDto> Accounts { get; set; } = new();
        public decimal TotalBalance { get; set; }
    }

}
