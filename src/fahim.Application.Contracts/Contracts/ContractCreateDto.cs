using System;
using System.ComponentModel.DataAnnotations;

namespace fahim.Contracts
{
    public class ContractCreateDto
    {
        public string Name { get; set; }
        public int? ContractId { get; set; }
    }
}