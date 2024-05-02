using Wz.ReservingSystem.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Wz.ReservingSystem.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(ReservingSystemEntityFrameworkCoreModule),
    typeof(ReservingSystemApplicationContractsModule)
    )]
public class ReservingSystemDbMigratorModule : AbpModule
{
}
