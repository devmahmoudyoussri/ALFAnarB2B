using System;
using Volo.Abp.Application.Dtos;

namespace fahim.Contracts
{
    public class ContractDto : FullAuditedEntityDto<int>
    {
        public string Name { get; set; }
        public int? ContractId { get; set; }
    }
}