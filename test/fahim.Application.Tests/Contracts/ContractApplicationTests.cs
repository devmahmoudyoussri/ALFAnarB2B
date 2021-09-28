using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace fahim.Contracts
{
    public class ContractsAppServiceTests : fahimApplicationTestBase
    {
        private readonly IContractsAppService _contractsAppService;
        private readonly IRepository<Contract, int> _contractRepository;

        public ContractsAppServiceTests()
        {
            _contractsAppService = GetRequiredService<IContractsAppService>();
            _contractRepository = GetRequiredService<IRepository<Contract, int>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _contractsAppService.GetListAsync(new GetContractsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Contract.Id == 968540307).ShouldBe(true);
            result.Items.Any(x => x.Contract.Id == 1172618526).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _contractsAppService.GetAsync(968540307);

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(968540307);
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ContractCreateDto
            {
                Name = "348c806ed77a45348dff1e9e9ff3fe6929b7f4c5376544efa294ad4e1bde345aa8c43925fb3544df8afeeabe2857a3b22a1"
            };

            // Act
            var serviceResult = await _contractsAppService.CreateAsync(input);

            // Assert
            var result = await _contractRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("348c806ed77a45348dff1e9e9ff3fe6929b7f4c5376544efa294ad4e1bde345aa8c43925fb3544df8afeeabe2857a3b22a1");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ContractUpdateDto()
            {
                Name = "9404e9b2a21"
            };

            // Act
            var serviceResult = await _contractsAppService.UpdateAsync(968540307, input);

            // Assert
            var result = await _contractRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("9404e9b2a21");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _contractsAppService.DeleteAsync(968540307);

            // Assert
            var result = await _contractRepository.FindAsync(c => c.Id == 968540307);

            result.ShouldBeNull();
        }
    }
}