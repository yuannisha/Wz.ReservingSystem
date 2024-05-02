using System;
using System.Collections.Generic;
using Wz.ReservingSystem.Enums;

namespace Wz.ReservingSystem.ReservingOperationIAppservice;

public class GetRecordingsOutput
{
    public List<Item> GetRecordingsOutputDto { get; set; }
}

public class Item
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string IDNumber { get; set; }
    public string BuildingAndFloor { get; set; }
    public string Classroom { get; set; }
    public string ReservingTime { get; set; }
    public string ReservingWeekday { get; set; }
    public ReservingStatusEnum ReservingStatus { get; set; }
}