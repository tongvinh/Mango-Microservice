using Mango.Web.Models;

namespace Mango.Services.ShoppingCartAPI.Service.IService;

public interface ICouponService
{
    Task<CouponDto> GetCoupon(string couponCode);
}