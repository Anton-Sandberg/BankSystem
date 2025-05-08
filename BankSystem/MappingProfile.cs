using AutoMapper;
using BankSystem.ViewModels;
using Services.DTOs;

namespace BankSystem
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CountryBalanceDto, CountryBalanceViewModel>()
                    .ReverseMap();

            CreateMap<CustomerSearchResultDto, CustomerSearchViewModel>().ReverseMap();

            CreateMap<CustomerDetailsDto, CustomerDetailsViewModel>().ReverseMap();

            CreateMap<AccountDto, AccountViewModel>().ReverseMap();

            CreateMap<TransactionDto, TransactionViewModel>().ReverseMap();
        }
    }
}
