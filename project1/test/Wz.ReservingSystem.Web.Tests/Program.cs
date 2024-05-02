using Microsoft.AspNetCore.Builder;
using Wz.ReservingSystem;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
await builder.RunAbpModuleAsync<ReservingSystemWebTestModule>();

public partial class Program
{
}
