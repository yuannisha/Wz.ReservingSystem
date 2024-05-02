using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Wz.ReservingSystem.CustomUserIAppservice;
using Wz.ReservingSystem.ReservingOperationIAppservice;
using Wz.ReservingSystem.TimeSlot;

namespace Wz.ReservingSystem.ReservingOperationController;
[Route("ReservingOperation")]
public class ReservingOperationControllers(ReservingOperationIAppservices reservingOperationIAppservices)
    : AbpController, ReservingOperationIAppservices
{
    [HttpPost("Reserving")]
    public async Task<RegisterOrResetPasswordOutDto> Reserving(string Name, string IDNumber, string BuildingAndFloor, string Classroom, string ReservingTime)
    {
        return await reservingOperationIAppservices.Reserving(Name, IDNumber, BuildingAndFloor, Classroom,
            ReservingTime);
    }
    [HttpPost("GetTimesByDate")]
    public async Task<Dictionary<string, List<TimeSlots>>> GetTimesByDate(string Date)
    {
        return await reservingOperationIAppservices.GetTimesByDate(Date);
    }
    [HttpPost("GetPreviewInformation")]
    public async Task<Dictionary<string, Infor>> GetPreviewInformation()
    {
        return await reservingOperationIAppservices.GetPreviewInformation();
    }
    [HttpPost("GetRecordingsByIDNumber")]
    public async Task<GetRecordingsOutput> GetRecordingsByIDNumber(string IDNumber)
    {
        return await reservingOperationIAppservices.GetRecordingsByIDNumber(IDNumber);
    }
    [HttpPost("CancleReserving")]
    public async Task<GetRecordingsOutput> CancleReserving(Guid Id, string IDNumber)
    {
        return await reservingOperationIAppservices.CancleReserving(Id, IDNumber);
    }
    [HttpPost("DeleteReserving")]
    public async Task<GetRecordingsOutput> DeleteReserving(Guid Id, string IDNumber)
    {
        return await reservingOperationIAppservices.DeleteReserving(Id, IDNumber);
    }
}