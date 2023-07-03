using Mango.Services.AuthAPI.Data;
using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Services.IService;
using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Services;

public class AuthService: IAuthService
{
    private readonly AppDbContext _db;
    private readonly ApplicationUser _userManager;
    private readonly IdentityRole _roleManage;

    public AuthService(AppDbContext db, ApplicationUser userManager, IdentityRole roleManage)
    {
        _db = db;
        _userManager = userManager;
        _roleManage = roleManage;
    }
    public Task<UserDto> Register(RegisterationRequestDto registerationRequestDto)
    {
        throw new NotImplementedException();
    }

    public Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
    {
        throw new NotImplementedException();
    }
}