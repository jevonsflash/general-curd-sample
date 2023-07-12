using System.Threading.Tasks;

namespace Matoapp.Data;

public interface IMatoappDbSchemaMigrator
{
    Task MigrateAsync();
}
