using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace fahim.Data
{
    /* This is used if database provider does't define
     * IfahimDbSchemaMigrator implementation.
     */
    public class NullfahimDbSchemaMigrator : IfahimDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}