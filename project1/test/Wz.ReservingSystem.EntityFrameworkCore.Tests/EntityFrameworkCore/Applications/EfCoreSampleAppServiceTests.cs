using Wz.ReservingSystem.Samples;
using Xunit;

namespace Wz.ReservingSystem.EntityFrameworkCore.Applications;

[Collection(ReservingSystemTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<ReservingSystemEntityFrameworkCoreTestModule>
{

}
