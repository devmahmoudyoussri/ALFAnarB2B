using fahim.Shared;
using fahim.Contracts;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using fahim.Permissions;
using fahim.Contracts;

namespace fahim.Contracts
{
    [RemoteService(IsEnabled = false)]
    [Authorize(fahimPermissions.Contracts.Default)]
    public class ContractsAppService : ApplicationService, IContractsAppService
    {
        private readonly IContractRepository _contractRepository;

        public ContractsAppService(IContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }

        public virtual async Task<PagedResultDto<ContractWithNavigationPropertiesDto>> GetListAsync(GetContractsInput input)
        {
            var totalCount = await _contractRepository.GetCountAsync(input.FilterText, input.Name, input.ContractId);
            var items = await _contractRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Name, input.ContractId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<ContractWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ContractWithNavigationProperties>, List<ContractWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<ContractWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(int id)
        {
            return ObjectMapper.Map<ContractWithNavigationProperties, ContractWithNavigationPropertiesDto>
                (await _contractRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<ContractDto> GetAsync(int id)
        {
            return ObjectMapper.Map<Contract, ContractDto>(await _contractRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<int?>>> GetContractLookupAsync(LookupRequestDto input)
        {
            var query = _contractRepository.AsQueryable()
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Name != null &&
                         x.Name.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Contract>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<int?>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Contract>, List<LookupDto<int?>>>(lookupData)
            };
        }

        [Authorize(fahimPermissions.Contracts.Delete)]
        public virtual async Task DeleteAsync(int id)
        {
            await _contractRepository.DeleteAsync(id);
        }

        [Authorize(fahimPermissions.Contracts.Create)]
        public virtual async Task<ContractDto> CreateAsync(ContractCreateDto input)
        {

            var contract = ObjectMapper.Map<ContractCreateDto, Contract>(input);

            contract = await _contractRepository.InsertAsync(contract, autoSave: true);
            return ObjectMapper.Map<Contract, ContractDto>(contract);
        }

        [Authorize(fahimPermissions.Contracts.Edit)]
        public virtual async Task<ContractDto> UpdateAsync(int id, ContractUpdateDto input)
        {

            var contract = await _contractRepository.GetAsync(id);
            ObjectMapper.Map(input, contract);
            contract = await _contractRepository.UpdateAsync(contract, autoSave: true);
            return ObjectMapper.Map<Contract, ContractDto>(contract);
        }
    }
}