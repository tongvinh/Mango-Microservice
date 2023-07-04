using Mango.Web.Models;
using Mango.Web.Models.Dto;
using Mango.Web.Service.IService;
using Mango.Web.Utility;

namespace Mango.Web.Service;

public class AuthService:IAuthService
{
    private readonly IBaseService _baseService;

    public AuthService(IBaseService baseService)
    {
        _baseService = baseService;
    }
    public async Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = SD.ApiType.POST,
            Data = loginRequestDto,
            Url = SD.AuthAPIBase + "/api/auth/Login"
        });
    }

    public Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseDto?> AssignRoleAsync(RegistrationRequestDto registrationRequestDto)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = SD.ApiType.POST,
            Data = registrationRequestDto,
            Url = SD.AuthAPIBase + "/api/auth/AssignRole"
        });
    }
}