using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace fahim.Contracts
{
    public interface IContractRepository : IRepository<Contract, int>
    {
        Task<ContractWithNavigationProperties> GetWithNavigationPropertiesAsync(
    int id,
    CancellationToken cancellationToken = default
);

        Task<List<ContractWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string name = null,
            int? contractId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<Contract>> GetListAsync(
                    string filterText = null,
                    string name = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            string name = null,
            int? contractId = null,
            CancellationToken cancellationToken = default);
    }
}