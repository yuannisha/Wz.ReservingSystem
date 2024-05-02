using System;
using System.Threading.Tasks;
using Volo.Abp;

namespace Wz.ReservingSystem.CustomUserIAppservice;

public interface CustomUserIAppservices
{
    public Task<RegisterOrResetPasswordOutDto> Register(string Name,string IDNumber,string Class,string Password);
    public Task<LoginOutputDto> Login(string IDNumber,string Password);
    public Task<RegisterOrResetPasswordOutDto> ResetPassword(string IDNumber,string NewPassword);
    // public Task LogoutAsync();
}

public class RegisterOrResetPasswordOutDto
{
    public bool SuccessfullyOrNot { get; set; }
    public string Tips { get; set; }
}