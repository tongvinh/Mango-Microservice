using Mango.Services.ShoppingCartAPI.Models.Dto;

namespace Mango.Services.ShoppingCartAPI.Service.IService;

public class ProductService:IProductService
{
    public Task<IEnumerable<ProductDto>> GetProducts()
    {
        throw new NotImplementedException();
    }
}