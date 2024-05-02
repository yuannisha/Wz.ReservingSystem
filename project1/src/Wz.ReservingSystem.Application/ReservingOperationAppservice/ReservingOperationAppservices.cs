using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using Wz.ReservingSystem.CustomUserIAppservice;
using Wz.ReservingSystem.Enums;
using Wz.ReservingSystem.ReservingOperation;
using Wz.ReservingSystem.ReservingOperationIAppservice;
using Wz.ReservingSystem.TimeSlot;
using Wz.ReservingSystem.Users;

namespace Wz.ReservingSystem.ReservingOperationAppservice;

public class ReservingOperationAppservices : AbpController , ReservingOperationIAppservices
{
    private readonly IRepository<ReservingInformation, Guid> _repository;
    private readonly IRepository<ReservingCapCount, Guid> _reservingCapCountRepository;
    private List<string> Classrooms = new List<string>() { "琴房501","琴房502","琴房503",
        "琴房504","琴房601","琴房602","琴房603","琴房604","琴房701","琴房702","琴房703","琴房704"};
    private readonly IObjectMapper _objectMapper;
    public ReservingOperationAppservices(IRepository<ReservingInformation, Guid> repository,IObjectMapper objectMapper,
        IRepository<ReservingCapCount, Guid> reservingCapCountRepository)
    {
        _repository = repository;
        _objectMapper = objectMapper;
        _reservingCapCountRepository = reservingCapCountRepository;
    }
    

    public async Task<RegisterOrResetPasswordOutDto> Reserving(string Name, string IDNumber, string BuildingAndFloor, string Classroom,
        string ReservingTime)
    {
        var result = new RegisterOrResetPasswordOutDto() { SuccessfullyOrNot = true, Tips = "恭喜你，预约成功！" };
        var ttp = ReservingTime.Split(" ");
        var weekday = ttp[2];
        var reservingTime = ttp[0] + " " + ttp[1];

        var temp1 = await _reservingCapCountRepository.GetListAsync();
        var temp0 = temp1.Where(x => x.IDNumber.Equals(IDNumber)).ToList();
        if (temp0.Count.Equals(0))
        {
            await _repository.InsertAsync(new ReservingInformation(){BuildingAndFloor = BuildingAndFloor,
                Classroom = Classroom,Name = Name,IDNumber = IDNumber,ReservingTime = reservingTime,ReservingWeekday = 
                    weekday,ReservingStatus = ReservingStatusEnum.HasReserved});

            await _reservingCapCountRepository.InsertAsync(new ReservingCapCount(){Date = ttp[0],IDNumber = IDNumber,
                ReservingTimesCount = 1});
        }
        else
        {
            var temp3 = temp0.Find(x => x.Date.Equals(ttp[0]));
            if (temp3 == null)
            {
                await _repository.InsertAsync(new ReservingInformation(){BuildingAndFloor = BuildingAndFloor,
                    Classroom = Classroom,Name = Name,IDNumber = IDNumber,ReservingTime = reservingTime,ReservingWeekday = 
                        weekday,ReservingStatus = ReservingStatusEnum.HasReserved});

                await _reservingCapCountRepository.InsertAsync(new ReservingCapCount(){Date = ttp[0],IDNumber = IDNumber,
                    ReservingTimesCount = 1});
            }
            else
            {
                if (temp3.ReservingTimesCount + 1 > 3)
                {
                    result.SuccessfullyOrNot = false;
                    result.Tips = "抱歉，每人每天只能预约3小时！";
                }
                else
                {
                    await _repository.InsertAsync(new ReservingInformation(){BuildingAndFloor = BuildingAndFloor,
                        Classroom = Classroom,Name = Name,IDNumber = IDNumber,ReservingTime = reservingTime,ReservingWeekday = 
                            weekday,ReservingStatus = ReservingStatusEnum.HasReserved});
                    temp3.ReservingTimesCount++;
                }
            }
        }
        return result;
    }


    public async Task<Dictionary<string, List<TimeSlots>>> GetTimesByDate(string Date)
    {
        var res = new Dictionary<string, List<TimeSlots>>();
        foreach (var classroom in Classrooms)
        {
            var times = GenerateHourlyTimeSlots();
            var timeslots = new List<TimeSlots>();
            times.ForEach(x =>
            {
                timeslots.Add(new TimeSlots(){Time = x,Surplus = 1});
            });
            res.Add(classroom,timeslots);
        }

        var rrr = await _repository.GetListAsync();
        var rrrRes = rrr.Where(x =>
            x.ReservingTime.Contains(Date) && x.ReservingStatus.Equals(ReservingStatusEnum.HasReserved)).ToList();
        // var rrrRes = rrr.ToList();
        if (!rrrRes.Count.Equals(0))
        {
            foreach (var reservingInformation in rrrRes)
            {
                var slot = reservingInformation.ReservingTime.Split(" ")[1];
                res[reservingInformation.Classroom].ForEach(x =>
                {
                    if (x.Time.Equals(slot))
                        x.Surplus = 0;
                });
            }
        }

        return res;
    }


    public async Task<Dictionary<string, Infor>> GetPreviewInformation()
    {
        var res = new Dictionary<string, Infor>();
        var temp1 = GenerateNextWeekDays();
        var dates = temp1.Keys.ToList();
        foreach (var date in dates)
        {
            var rrr = await GetTimesByDate(date);
            var sum = 0;
            var lists = rrr.Values.ToList();
            foreach (var list in lists)
            {
                list.ForEach(x =>
                {
                    if(!x.Surplus.Equals(0))
                        sum += 1;
                });
                // sum += list.Count;
            }
            res.Add(date,new Infor(){WeekDay = temp1[date],RestSums = sum});
        }

        return res;
    }


    public async Task<GetRecordingsOutput> GetRecordingsByIDNumber(string IDNumber)
    {
        var rrr =await _repository.GetListAsync(x => x.IDNumber.Equals(IDNumber));
        // var rrrRes = rrr.ToList();
        var res = _objectMapper.Map<List<ReservingInformation>, List<Item>>(rrr);
        return new GetRecordingsOutput() { GetRecordingsOutputDto = res };
    }


    public async Task<GetRecordingsOutput> CancleReserving(Guid Id,string IDNumber)
    {
        var record = await _repository.FindAsync(Id) ?? throw new Exception("记录不存在！");
        record.ReservingStatus = ReservingStatusEnum.HasBeenCancled;
        var tprt = record.ReservingTime.Split(" ");
        var ttp =await _reservingCapCountRepository.FindAsync(x=>
                x.IDNumber.Equals(IDNumber) && x.Date.Equals(tprt[0]));
        if(ttp.ReservingTimesCount.Equals(0))
            ttp.ReservingTimesCount --;
        await _reservingCapCountRepository.UpdateAsync(ttp);
        await _repository.UpdateAsync(record);
        return await GetRecordingsByIDNumber(IDNumber);
    }

    public async Task<GetRecordingsOutput> DeleteReserving(Guid Id, string IDNumber)
    {
        var entity = await _repository.FindAsync(Id) ?? throw new Exception("抱歉查找此项失败！");
        var tprt = entity.ReservingTime.Split(" ");
        var ttp =await _reservingCapCountRepository.FindAsync(x=>
            x.IDNumber.Equals(IDNumber) && x.Date.Equals(tprt[0]));
        if(ttp.ReservingTimesCount.Equals(0))
            ttp.ReservingTimesCount --;
        await _reservingCapCountRepository.UpdateAsync(ttp);
        await _repository.DeleteAsync(entity);
        return await GetRecordingsByIDNumber(IDNumber);
    }

    private List<string> GenerateHourlyTimeSlots()
    {
        List<string> timeSlots = new List<string>();
        for (int hour = 8; hour < 22; hour++)
        {
            string start = "";
            if(hour < 10)
                start = $"0{hour}:00";
            else
                start = $"{hour}:00";
            string end = $"{hour + 1:00}:00";
            timeSlots.Add($"{start}-{end}");
        }
        return timeSlots;
    }
    
    private Dictionary<string, string> GenerateNextWeekDays()
    {
        var days = new Dictionary<string, string>();
        var startDate = DateTime.Now.AddDays(1); // Start from tomorrow
        CultureInfo ci = new CultureInfo("zh-CN");

        for (int i = 0; i < 7; i++)
        {
            DateTime currentDate = startDate.AddDays(i);
            string dateString = currentDate.ToString("yyyy-MM-dd");
            string dayOfWeek = currentDate.ToString("dddd", new CultureInfo("zh-CN"));
            days.Add(dateString, dayOfWeek);
        }

        return days;
    }
    
}