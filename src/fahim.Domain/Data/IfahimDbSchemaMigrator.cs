using System.Threading.Tasks;

namespace fahim.Data
{
    public interface IfahimDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}