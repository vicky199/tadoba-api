using AutoMapper;
using tadoba_api.Entity;
using tadoba_api.Models;
using tadoba_api.Repository;
using tadoba_api.Uow;

namespace tadoba_api.Services
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductService(IGenericRepository<Product> productRepo, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _productRepo = productRepo;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<Response<ProductModel>> AddProduct(ProductModel productModel)
        {
            Product product = _mapper.Map<Product>(productModel);
            product = await _productRepo.AddAsync(product);
            productModel = _mapper.Map<ProductModel>(product);
            return await Response<ProductModel>.GenerateResponse(true, productModel, "Product Added Successfully.");
        }

        public async Task<Response<IEnumerable<ProductModel>>> GetAllProduct()
        {
            List<ProductModel> result = new List<ProductModel>();
            List<Product> products = (await _productRepo.ListAllAsync(x => x.IsActive)).ToList();
            if (products.Any())
            {
                result = _mapper.Map<List<ProductModel>>(products);
                return await Response<IEnumerable<ProductModel>>.GenerateResponse(true, result);
            }
            else
            {
                return await Response<IEnumerable<ProductModel>>.GenerateResponse(false, result, "No product available.");
            }
        }

        public async Task<Response<ProductModel>> UpdateProduct(ProductModel productModel)
        {
            Product product = _mapper.Map<Product>(productModel);
            await _productRepo.UpdateAsync(product);
            productModel = _mapper.Map<ProductModel>(product);
            return await Response<ProductModel>.GenerateResponse(true, productModel, "Product Update Successfully.");
        }
    }
}
