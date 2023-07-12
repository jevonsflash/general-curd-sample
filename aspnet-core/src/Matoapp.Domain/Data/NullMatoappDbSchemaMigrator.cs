using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Matoapp.Data;

/* This is used if database provider does't define
 * IMatoappDbSchemaMigrator implementation.
 */
public class NullMatoappDbSchemaMigrator : IMatoappDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
