using Volo.Abp.Modularity;

namespace Wz.ReservingSystem;

public abstract class ReservingSystemApplicationTestBase<TStartupModule> : ReservingSystemTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
