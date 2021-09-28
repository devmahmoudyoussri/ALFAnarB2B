using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using fahim.Data;
using Volo.Abp.DependencyInjection;

namespace fahim.EntityFrameworkCore
{
    public class EntityFrameworkCorefahimDbSchemaMigrator
        : IfahimDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCorefahimDbSchemaMigrator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the fahimDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<fahimDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}
