using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Wz.ReservingSystem.Data;

/* This is used if database provider does't define
 * IReservingSystemDbSchemaMigrator implementation.
 */
public class NullReservingSystemDbSchemaMigrator : IReservingSystemDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
