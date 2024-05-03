using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wz.ReservingSystem.CustomUserIAppservice;
using Wz.ReservingSystem.TimeSlot;

namespace Wz.ReservingSystem.ReservingOperationIAppservice;

public interface ReservingOperationIAppservices
{
    public Task<ReservingOutputDto> Reserving(string Name,string IDNumber,
        string BuildingAndFloor,string Classroom,string ReservingTime);

    public Task<Dictionary<string,List<TimeSlots>>> GetTimesByDate(string Date);

    public Task<Dictionary<string, Infor>> GetPreviewInformation();

    public Task<GetRecordingsOutput> GetRecordingsByIDNumber(string IDNumber);
    
    public Task<GetRecordingsOutput> CancleReserving(Guid Id,string IDNumber);

    public Task<GetRecordingsOutput> DeleteReserving(Guid Id,string IDNumber);
}

public class Infor
{
    public string WeekDay { get; set; }
    public int RestSums { get; set; }
}