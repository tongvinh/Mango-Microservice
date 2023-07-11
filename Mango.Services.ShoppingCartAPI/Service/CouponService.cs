using Mango.Services.ShoppingCartAPI.Service.IService;
using Mango.Web.Models;

namespace Mango.Services.ShoppingCartAPI.Service;

public class CouponService:ICouponService
{
    public Task<CouponDto> GetCoupon(string couponCode)
    {
        throw new NotImplementedException();
    }
}