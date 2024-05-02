using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Wz.ReservingSystem.Data;
using Volo.Abp.DependencyInjection;

namespace Wz.ReservingSystem.EntityFrameworkCore;

public class EntityFrameworkCoreReservingSystemDbSchemaMigrator
    : IReservingSystemDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreReservingSystemDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the ReservingSystemDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<ReservingSystemDbContext>()
            .Database
            .MigrateAsync();
    }
}
