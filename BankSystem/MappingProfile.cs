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
                    .ForMember(dest => dest.CountryCode, opt => opt.MapFrom(src => CountryFlagHelper.GetCountryCode(src.Country)))
                    .ReverseMap();

            CreateMap<CustomerSearchResultDto, CustomerSearchViewModel>().ReverseMap();

            CreateMap<CustomerDetailsDto, CustomerDetailsViewModel>().ReverseMap();

            CreateMap<AccountDto, AccountViewModel>().ReverseMap();

            CreateMap<TransactionDto, TransactionViewModel>().ReverseMap();
        }
    }
}
