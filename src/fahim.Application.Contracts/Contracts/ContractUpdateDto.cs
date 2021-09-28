using System;
using System.ComponentModel.DataAnnotations;

namespace fahim.Contracts
{
    public class ContractUpdateDto
    {
        public string Name { get; set; }
        public int? ContractId { get; set; }
    }
}