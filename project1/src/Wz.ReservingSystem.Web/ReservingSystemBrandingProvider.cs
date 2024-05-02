using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Wz.ReservingSystem.Web;

[Dependency(ReplaceServices = true)]
public class ReservingSystemBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "ReservingSystem";
}
