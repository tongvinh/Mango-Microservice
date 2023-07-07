using System.Net;
using System.Text;
using Mango.Web.Models;
using Mango.Web.Service.IService;
using static Mango.Web.Utility.SD;
using Newtonsoft.Json;

namespace Mango.Web.Service;

public class BaseService: IBaseService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ITokenProvider _tokenProvider;

    public BaseService(IHttpClientFactory httpClientFactory, ITokenProvider tokenProvider)
    {
        _httpClientFactory = httpClientFactory;
        _tokenProvider = tokenProvider;
    }
    public async Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true)    
    {
        try
        {
            HttpClient client = _httpClientFactory.CreateClient("MangoAPI");
        HttpRequestMessage message = new();
        message.Headers.Add("Accept","application/json");
        //token
        if (withBearer)
        {
            var token = _tokenProvider.GetToken();
            message.Headers.Add("Authorization",$"Bearer {token}");
        }

        message.RequestUri = new Uri(requestDto.Url);
        if (requestDto.Data != null)
        {
            message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8,
                "application/json");
        }

        HttpResponseMessage? apiResponse = null;

        message.Method = requestDto.ApiType switch
        {
            ApiType.POST => HttpMethod.Post,
            ApiType.DELETE => HttpMethod.Delete,
            ApiType.PUT => HttpMethod.Put,
            _ => HttpMethod.Get
        };

        apiResponse = await client.SendAsync(message);

        switch (apiResponse.StatusCode)
        {
            case HttpStatusCode.NotFound:
                return new() { IsSuccess = false, Message = "Not Found" };
            case HttpStatusCode.Forbidden:
                return new() { IsSuccess = false, Message = "Access Denied" };
            case HttpStatusCode.Unauthorized:
                return new() { IsSuccess = false, Message = "Unauthorized" };
            case HttpStatusCode.InternalServerError:
                return new() { IsSuccess = false, Message = "Internal Server Error" };
            default:
                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                return apiResponseDto;
        }
        }
        catch (Exception ex)
        {
            var dto = new ResponseDto()
            {
                Message = ex.Message,
                IsSuccess = false
            };
            return dto;
        }
    }
}