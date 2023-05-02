using tadoba_api.Models;

namespace tadoba_api.Services
{
    public interface IProductService
    {
        Task<Response<IEnumerable<ProductModel>>> GetAllProduct();
        Task<Response<ProductModel>> AddProduct(ProductModel productModel);
        Task<Response<ProductModel>> UpdateProduct(ProductModel productModel);
    }
}
