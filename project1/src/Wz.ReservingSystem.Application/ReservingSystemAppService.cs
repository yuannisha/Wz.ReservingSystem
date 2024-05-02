using System;
using System.Collections.Generic;
using System.Text;
using Wz.ReservingSystem.Localization;
using Volo.Abp.Application.Services;

namespace Wz.ReservingSystem;

/* Inherit your application services from this class.
 */
public abstract class ReservingSystemAppService : ApplicationService
{
    protected ReservingSystemAppService()
    {
        LocalizationResource = typeof(ReservingSystemResource);
    }
}
