using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Wz.ReservingSystem.ReservingOperation;
using Wz.ReservingSystem.Users;

namespace Wz.ReservingSystem.EntityFrameworkCore.ReservingSystemDbContextExtension;

public static class ReservingSystemDbContextExtension
{
    public static void ReservingSystemDbContextExtensionModel(this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
        builder.Entity<User>(b =>
        {
            b.ToTable("CustomUser");
            b.Property(e => e.IDNumber).IsRequired().HasMaxLength(13);
            b.Property(e => e.Class).IsRequired().HasMaxLength(15);
            b.Property(e => e.Name).IsRequired().HasMaxLength(5);
            b.Property(e => e.Password).IsRequired();
            b.ConfigureByConvention();
        });
        
        builder.Entity<ReservingInformation>(b =>
        {
            b.ToTable("ReservingInformation");
            b.Property(e => e.IDNumber).IsRequired().HasMaxLength(13);
            b.Property(e => e.Name).IsRequired().HasMaxLength(5);
            b.Property(e => e.BuildingAndFloor).IsRequired();
            b.Property(e => e.ReservingTime).IsRequired();
            b.Property(e => e.ReservingWeekday).IsRequired();
            b.Property(e => e.ReservingStatus).IsRequired();
            b.Property(e => e.Classroom).IsRequired();
            b.ConfigureByConvention();
        });
        
        builder.Entity<ReservingCapCount>(b =>
        {
            b.ToTable("ReservingCapCount");
            b.Property(e => e.IDNumber).IsRequired().HasMaxLength(13);
            b.Property(e => e.Date).IsRequired().HasMaxLength(20);
            b.Property(e => e.ReservingTimesCount).IsRequired();
            b.ConfigureByConvention();
        });
        
    }
}