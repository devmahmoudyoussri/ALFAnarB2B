using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using fahim.Contracts;

namespace fahim.Contracts
{
    public class ContractsDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IContractRepository _contractRepository;

        public ContractsDataSeedContributor(IContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            await _contractRepository.InsertAsync(new Contract
            (
                id: 968540307,
                name: "2747281e16e"
            ));

            await _contractRepository.InsertAsync(new Contract
            (
                id: 1172618526,
                name: "2eca487f581b4db8b549be88fe388802"
            ));
        }
    }
}