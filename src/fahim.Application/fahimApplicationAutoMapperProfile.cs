using System;
using fahim.Shared;
using Volo.Abp.AutoMapper;
using fahim.Contracts;
using AutoMapper;

namespace fahim
{
    public class fahimApplicationAutoMapperProfile : Profile
    {
        public fahimApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

            CreateMap<ContractCreateDto, Contract>().IgnoreFullAuditedObjectProperties().Ignore(x => x.ExtraProperties).Ignore(x => x.ConcurrencyStamp).Ignore(x => x.Id);
            CreateMap<ContractUpdateDto, Contract>().IgnoreFullAuditedObjectProperties().Ignore(x => x.ExtraProperties).Ignore(x => x.ConcurrencyStamp).Ignore(x => x.Id);
            CreateMap<Contract, ContractDto>();

            CreateMap<ContractWithNavigationProperties, ContractWithNavigationPropertiesDto>();
            CreateMap<Contract, LookupDto<int?>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));
        }
    }
}