using Wz.ReservingSystem.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Wz.ReservingSystem.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class ReservingSystemController : AbpControllerBase
{
    protected ReservingSystemController()
    {
        LocalizationResource = typeof(ReservingSystemResource);
    }
}
