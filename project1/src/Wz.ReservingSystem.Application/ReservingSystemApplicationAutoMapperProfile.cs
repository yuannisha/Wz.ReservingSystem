using AutoMapper;
using Wz.ReservingSystem.CustomUserIAppservice;
using Wz.ReservingSystem.ReservingOperation;
using Wz.ReservingSystem.ReservingOperationIAppservice;
using Wz.ReservingSystem.Users;

namespace Wz.ReservingSystem;

public class ReservingSystemApplicationAutoMapperProfile : Profile
{
    public ReservingSystemApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<User, LoginOutputDto>();
        CreateMap<ReservingInformation, LoginOutputDto>().AfterMap((src, dest) =>
        {
            dest.BuildingAndFloor = src.BuildingAndFloor;
            dest.Classroom = src.Classroom;
            dest.ReservingStatus = src.ReservingStatus;
            dest.ReservingTime = src.ReservingTime;
        });
        CreateMap<ReservingInformation, Item>();
        CreateMap<ReservingInformation, ReservingOutputDto>();
    }
}
