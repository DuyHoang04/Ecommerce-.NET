using Ecommerce.Dto.Request;
using Ecommerce.Dto.Response;
using Ecommerce.Dto.Wrapper;
using Ecommerce.Repository.ProductRepository;

namespace Ecommerce.Service.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ResponseDto<string>> CreateProduct(ProductRequest request)
        {
            return await _productRepository.CreateProduct(request);
        }

      
        public async Task<ResponseDto<string>> DeleteProduct(int productId)
        {
            return await _productRepository.DeleteProduct(productId);
        }

        public async Task<byte[]> ExportFileExcel(CancellationToken ct)
        {
            return await _productRepository.ExportFileExcel(ct);
        }

        public async Task<ResponseDto<List<ProductWrapper>>> FindAllProduct(int page, int size, string searchName, int? categoryId, int? brandId)
        {
            return await _productRepository.FindAllProduct(page, size, searchName, categoryId, brandId);
        }

        public async Task<ResponseDto<ProductDetailWrapper>> FindProductById(int productId)
        {
            return await _productRepository.FindProductById(productId);
        }

        public async Task<ResponseDto<string>> UpdateProduct(ProductRequest request, int productId)
        {
            return await _productRepository.UpdateProduct(request, productId);
        }
    }
}
