using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using Wz.ReservingSystem.ReservingOperation;

namespace Wz.ReservingSystem.Users;

public class User : FullAuditedAggregateRoot<Guid>
{
    public string Name { get; set; }
    public string Password { get; set; }
    public string Class { get; set; }
    public string IDNumber { get; set; }

}