using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using fahim.Contracts;
using fahim.EntityFrameworkCore;
using Xunit;

namespace fahim.Contracts
{
    public class ContractRepositoryTests : fahimEntityFrameworkCoreTestBase
    {
        private readonly IContractRepository _contractRepository;

        public ContractRepositoryTests()
        {
            _contractRepository = GetRequiredService<IContractRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _contractRepository.GetListAsync(
                    name: "2747281e16e"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(968540307);
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _contractRepository.GetCountAsync(
                    name: "2eca487f581b4db8b549be88fe388802"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}