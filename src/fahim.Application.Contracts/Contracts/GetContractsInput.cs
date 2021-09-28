using Volo.Abp.Application.Dtos;
using System;

namespace fahim.Contracts
{
    public class GetContractsInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string Name { get; set; }
        public int? ContractId { get; set; }

        public GetContractsInput()
        {

        }
    }
}