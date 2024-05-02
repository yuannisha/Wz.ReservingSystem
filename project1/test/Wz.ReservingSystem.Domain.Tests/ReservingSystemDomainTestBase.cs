using Volo.Abp.Modularity;

namespace Wz.ReservingSystem;

/* Inherit from this class for your domain layer tests. */
public abstract class ReservingSystemDomainTestBase<TStartupModule> : ReservingSystemTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
