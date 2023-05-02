using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tadoba_api.Models;
using tadoba_api.Services;

namespace tadoba_api.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            return Ok(await _productService.GetAllProduct());
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductModel productModel)
        {
            return Ok(await _productService.AddProduct(productModel));
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(ProductModel productModel)
        {
            return Ok(await _productService.UpdateProduct(productModel));
        }
    }
}
