using Wz.ReservingSystem.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Wz.ReservingSystem.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class ReservingSystemPageModel : AbpPageModel
{
    protected ReservingSystemPageModel()
    {
        LocalizationResourceType = typeof(ReservingSystemResource);
    }
}
