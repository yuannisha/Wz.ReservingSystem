using System.Threading.Tasks;

namespace Wz.ReservingSystem.Data;

public interface IReservingSystemDbSchemaMigrator
{
    Task MigrateAsync();
}
