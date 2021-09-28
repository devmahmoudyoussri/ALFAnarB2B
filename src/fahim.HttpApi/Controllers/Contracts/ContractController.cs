using fahim.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using fahim.Contracts;

namespace fahim.Controllers.Contracts
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Contract")]
    [Route("api/app/contracts")]

    public class ContractController : AbpController, IContractsAppService
    {
        private readonly IContractsAppService _contractsAppService;

        public ContractController(IContractsAppService contractsAppService)
        {
            _contractsAppService = contractsAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<ContractWithNavigationPropertiesDto>> GetListAsync(GetContractsInput input)
        {
            return _contractsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<ContractWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(int id)
        {
            return _contractsAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<ContractDto> GetAsync(int id)
        {
            return _contractsAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("contract-lookup")]
        public Task<PagedResultDto<LookupDto<int?>>> GetContractLookupAsync(LookupRequestDto input)
        {
            return _contractsAppService.GetContractLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<ContractDto> CreateAsync(ContractCreateDto input)
        {
            return _contractsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<ContractDto> UpdateAsync(int id, ContractUpdateDto input)
        {
            return _contractsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(int id)
        {
            return _contractsAppService.DeleteAsync(id);
        }
    }
}