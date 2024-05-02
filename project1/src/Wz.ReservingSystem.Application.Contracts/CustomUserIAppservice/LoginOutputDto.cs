using Wz.ReservingSystem.Enums;

namespace Wz.ReservingSystem.CustomUserIAppservice;

public class LoginOutputDto
{
    public bool SuccessfullyOrNot { get; set; }
    public string Name { get; set; }
    public string Class { get; set; }
    public string IDNumber { get; set; }
    public string BuildingAndFloor { get; set; }
    public string Classroom { get; set; }
    public string ReservingTime { get; set; }
    public ReservingStatusEnum ReservingStatus { get; set; }
    public string LoginTips { get; set; }
    // public string Token { get; set; }
}