using fahim.Contracts;
using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;
using Volo.Abp;

namespace fahim.Contracts
{
    public class Contract : FullAuditedAggregateRoot<int>
    {
        [CanBeNull]
        public virtual string Name { get; set; }
        public int? ContractId { get; set; }

        public Contract()
        {

        }

        public Contract(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}