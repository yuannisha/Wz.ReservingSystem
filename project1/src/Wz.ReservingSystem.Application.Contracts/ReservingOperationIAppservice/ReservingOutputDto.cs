using System;
using System.Collections.Generic;
using System.Text;
using Wz.ReservingSystem.CustomUserIAppservice;
using Wz.ReservingSystem.Enums;

namespace Wz.ReservingSystem.ReservingOperationIAppservice
{
    public class ReservingOutputDto
    {
        public RegisterOrResetPasswordOutDto ResultWithTip { get; set; }

        public string BuildingAndFloor { get; set; }
        public string Classroom { get; set; }
        public string ReservingTime { get; set; }
        public ReservingStatusEnum ReservingStatus { get; set; }
    }
}
