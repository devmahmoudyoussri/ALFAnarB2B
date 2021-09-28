using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using fahim.EntityFrameworkCore;

namespace fahim.Contracts
{
    public class EfCoreContractRepository : EfCoreRepository<fahimDbContext, Contract, int>, IContractRepository
    {
        public EfCoreContractRepository(IDbContextProvider<fahimDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<ContractWithNavigationProperties> GetWithNavigationPropertiesAsync(int id, CancellationToken cancellationToken = default)
        {
            return await (await GetQueryForNavigationPropertiesAsync())
                .FirstOrDefaultAsync(e => e.Contract.Id == id, GetCancellationToken(cancellationToken));
        }

        public async Task<List<ContractWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string name = null,
            int? contractId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, name, contractId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ContractConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<ContractWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from contract in (await GetDbSetAsync())
                   join contract1 in (await GetDbContextAsync()).Contracts on contract.ContractId equals contract1.Id into contracts1
                   from contract1 in contracts1.DefaultIfEmpty()

                   select new ContractWithNavigationProperties
                   {
                       Contract = contract,
                       Contract1 = contract1
                   };
        }

        protected virtual IQueryable<ContractWithNavigationProperties> ApplyFilter(
            IQueryable<ContractWithNavigationProperties> query,
            string filterText,
            string name = null,
            int? contractId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Contract.Name.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Contract.Name.Contains(name))
                    .WhereIf(contractId != null, e => e.Contract != null && e.Contract.Id == contractId);
        }

        public async Task<List<Contract>> GetListAsync(
            string filterText = null,
            string name = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, name);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ContractConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string name = null,
            int? contractId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, name, contractId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Contract> ApplyFilter(
            IQueryable<Contract> query,
            string filterText,
            string name = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name));
        }
    }
}