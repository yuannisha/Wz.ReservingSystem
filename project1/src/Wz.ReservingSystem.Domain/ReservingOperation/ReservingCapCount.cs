using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Wz.ReservingSystem.Users;

namespace Wz.ReservingSystem.ReservingOperation;

public class ReservingCapCount : FullAuditedAggregateRoot<Guid>
{
    public string IDNumber { get; set; }
    public string Date { get; set; }
    public int ReservingTimesCount { get; set; }
    
}