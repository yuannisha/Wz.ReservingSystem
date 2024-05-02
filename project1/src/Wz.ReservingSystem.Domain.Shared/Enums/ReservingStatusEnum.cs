using System.ComponentModel;

namespace Wz.ReservingSystem.Enums;

public enum ReservingStatusEnum
{
    [Description("已预约")] HasReserved = 0,
    [Description("与取消预约")] HasBeenCancled = 1,
}