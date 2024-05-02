using Volo.Abp.Modularity;

namespace Wz.ReservingSystem;

[DependsOn(
    typeof(ReservingSystemDomainModule),
    typeof(ReservingSystemTestBaseModule)
)]
public class ReservingSystemDomainTestModule : AbpModule
{

}
