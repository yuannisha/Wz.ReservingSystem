using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Domain.Repositories;
using Wz.ReservingSystem.CustomUserIAppservice;
using Wz.ReservingSystem.Users;
using Volo.Abp.ObjectMapping;
using Wz.ReservingSystem.ReservingOperation;

namespace Wz.ReservingSystem.CustomUserAppservice;


public class CustomUserAppservices : AbpController , CustomUserIAppservices
{
    private readonly IRepository<User,Guid> _repository;
    private readonly IObjectMapper _objectMapper;
    private readonly IRepository<ReservingInformation, Guid> _repositoryOfReserving;

    private readonly IConfiguration _configuration;
    // private readonly SignInManager<User> _signInManager;
    public CustomUserAppservices(IRepository<User,Guid> repository,IObjectMapper objectMapper,
        IRepository<ReservingInformation, Guid> repositoryOfReserving,
        IConfiguration configuration
        // SignInManager<User> signInManager
            )
    {
        _repository = repository;
        _objectMapper = objectMapper;
        _repositoryOfReserving = repositoryOfReserving;
        _configuration = configuration;
        // _signInManager = signInManager;
    }


    public async Task<RegisterOrResetPasswordOutDto> Register(string Name, string IDNumber, string Class, string Password)
    {
        var user = await _repository.FirstOrDefaultAsync(p=>p.IDNumber.Equals(IDNumber));
        if (user != null)
            return new RegisterOrResetPasswordOutDto() { SuccessfullyOrNot = false, Tips = "此学号已被注册！" };
            // throw new Exception("此学号已被注册！");
        var passwordHash = GenerateSHA256Hash(Password);
        await _repository.InsertAsync(new User()
            { Class = Class, IDNumber = IDNumber, Name = Name, Password = passwordHash });
        // if (ss.Equals(null))
        //     throw new Exception("注册失败！");
        return new RegisterOrResetPasswordOutDto(){SuccessfullyOrNot = true,Tips = "恭喜你，注册成功！"};
    }


    public async Task<LoginOutputDto> Login(string IDNumber, string Password)
    {
        var user =await _repository.FirstOrDefaultAsync(p=>p.IDNumber.Equals(IDNumber) && p.Password.Equals
            (GenerateSHA256Hash(Password)));
        if (user == null)
            return new LoginOutputDto()
            {
                    LoginTips = "登录失败，请检查身份信息后重试！",SuccessfullyOrNot = false
            };
        
        // // 创建 JWT 令牌
        // var tokenHandler = new JwtSecurityTokenHandler();
        // var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecurityKey"]);
        // var tokenDescriptor = new SecurityTokenDescriptor
        // {
        //     Subject = new ClaimsIdentity(new Claim[]
        //     {
        //         new Claim(ClaimTypes.Name, user.IDNumber),
        //         // 其他自定义的声明
        //     }),
        //     Expires = RememberMe ? DateTime.UtcNow.AddDays(7) : DateTime.UtcNow.AddHours(1), // 或根据“记住我”来设定不同的过期时间
        //     SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        // };
        // var token = tokenHandler.CreateToken(tokenDescriptor);
        // var tokenString = tokenHandler.WriteToken(token);
        
        // var res1 = _objectMapper.Map<User, LoginOutputDto>(user);
        var res1 = new LoginOutputDto();
        var recording = await _repositoryOfReserving.GetListAsync(
            x => x.IDNumber.Equals(IDNumber));
        var recordingRes = recording.MaxBy(e => e.CreationTime);
        if(recordingRes != null)
            res1 = _objectMapper.Map<ReservingInformation, LoginOutputDto>(recordingRes);
        res1.LoginTips = "登录成功！";
        res1.SuccessfullyOrNot = true;
        res1.Name = user.Name;
        res1.Class = user.Class;
        res1.IDNumber = user.IDNumber;
        // res1.Token = tokenString;
        return res1;
      
    }


    public async Task<RegisterOrResetPasswordOutDto> ResetPassword(string IDNumber, string NewPassword)
    {
        var passwordHash = GenerateSHA256Hash(NewPassword);
        var user = await _repository.FindAsync(x=>x.IDNumber.Equals(IDNumber));
            // ?? throw new Exception("用户不存在");
        if (user == null) return new RegisterOrResetPasswordOutDto() { SuccessfullyOrNot = false, Tips = "用户不存在" };
            
        user.Password = passwordHash;
        await _repository.UpdateAsync(user);
        return new RegisterOrResetPasswordOutDto(){SuccessfullyOrNot = true,Tips = "更改密码成功，请重新登录！"};
    }


    // public async Task LogoutAsync()
    // {
    //     await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    //     Redirect("CustomUser/Login");
    // }

    private string GenerateSHA256Hash(string input)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            // 将输入字符串转换为字节数组
            byte[] bytes = Encoding.UTF8.GetBytes(input);

            // 计算哈希值
            byte[] hashBytes = sha256.ComputeHash(bytes);

            // 将哈希值转换为字符串表示
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                builder.Append(hashBytes[i].ToString("x2")); // 将每个字节转换为两位十六进制数，并追加到字符串中
            }
            return builder.ToString();
        }
    }
}