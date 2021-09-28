using fahim.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace fahim.Contracts
{
    public interface IContractsAppService : IApplicationService
    {
        Task<PagedResultDto<ContractWithNavigationPropertiesDto>> GetListAsync(GetContractsInput input);

        Task<ContractWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(int id);

        Task<ContractDto> GetAsync(int id);

        Task<PagedResultDto<LookupDto<int?>>> GetContractLookupAsync(LookupRequestDto input);

        Task DeleteAsync(int id);

        Task<ContractDto> CreateAsync(ContractCreateDto input);

        Task<ContractDto> UpdateAsync(int id, ContractUpdateDto input);
    }
}