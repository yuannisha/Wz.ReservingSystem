using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Wz.ReservingSystem.CustomUserIAppservice;

namespace Wz.ReservingSystem.UserController;
[Route("CustomUser")]
public class UserControllers(CustomUserIAppservices customUser) : AbpController, CustomUserIAppservices
{
    [HttpPost("Register")]
    public async Task<RegisterOrResetPasswordOutDto> Register(string Name, string IDNumber, string Class, string Password)
    {
        return await customUser.Register(Name, IDNumber, Class, Password);
    }

    [HttpPost("Login")]
    public async Task<LoginOutputDto> Login(string IDNumber, string Password)
    {
        return await customUser.Login(IDNumber, Password);
    }

    [HttpPost("ResetPassword")]
    public async Task<RegisterOrResetPasswordOutDto> ResetPassword(string IDNumber, string NewPassword)
    {
        return await customUser.ResetPassword(IDNumber, NewPassword);
    }
    // [HttpPost("LogoutAsync")]
    // public async Task LogoutAsync()
    // {
    //     await customUser.LogoutAsync();
    // }
}