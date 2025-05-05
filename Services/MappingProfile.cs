using AutoMapper;
using DataAccess.Models;
using Services.DTOs;

namespace Services
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerSearchResultDto>();

            CreateMap<Customer, CustomerDetailsDto>()
                .ForMember(dest => dest.Accounts, opt => opt.MapFrom(
                    src => src.Dispositions.Select(d => d.Account)))
                .ForMember(dest => dest.TotalBalance, opt => opt.MapFrom(
                    src => src.Dispositions.Sum(d => d.Account.Balance)))
                .ReverseMap();

            CreateMap<Account, AccountDto>().ReverseMap();

            CreateMap<Transaction, TransactionDto>()
                .ReverseMap();
        }
    }
}
