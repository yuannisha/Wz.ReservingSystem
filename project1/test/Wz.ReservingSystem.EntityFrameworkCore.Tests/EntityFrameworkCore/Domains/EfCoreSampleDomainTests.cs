using Wz.ReservingSystem.Samples;
using Xunit;

namespace Wz.ReservingSystem.EntityFrameworkCore.Domains;

[Collection(ReservingSystemTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<ReservingSystemEntityFrameworkCoreTestModule>
{

}
