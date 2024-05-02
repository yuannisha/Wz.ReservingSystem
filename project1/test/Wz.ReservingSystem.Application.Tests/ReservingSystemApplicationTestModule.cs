using Volo.Abp.Modularity;

namespace Wz.ReservingSystem;

[DependsOn(
    typeof(ReservingSystemApplicationModule),
    typeof(ReservingSystemDomainTestModule)
)]
public class ReservingSystemApplicationTestModule : AbpModule
{

}
