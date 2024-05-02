using System;
using Volo.Abp.Domain.Entities.Auditing;
using Wz.ReservingSystem.Enums;

namespace Wz.ReservingSystem.ReservingOperation;

public class ReservingInformation : FullAuditedAggregateRoot<Guid>
{
    public string Name { get; set; }
    public string IDNumber { get; set; }
    public string BuildingAndFloor { get; set; }
    public string Classroom { get; set; }
    public string ReservingTime { get; set; }
    public string ReservingWeekday { get; set; }
    public ReservingStatusEnum ReservingStatus { get; set; }
    
}