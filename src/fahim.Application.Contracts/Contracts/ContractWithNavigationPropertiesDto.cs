using fahim.Contracts;

using System;
using Volo.Abp.Application.Dtos;

namespace fahim.Contracts
{
    public class ContractWithNavigationPropertiesDto
    {
        public ContractDto Contract { get; set; }

        public ContractDto Contract1 { get; set; }

    }
}