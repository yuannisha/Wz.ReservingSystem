using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Wz.ReservingSystem.Pages;

public class Index_Tests : ReservingSystemWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
