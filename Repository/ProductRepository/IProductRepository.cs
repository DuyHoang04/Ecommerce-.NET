using Ecommerce.Dto.Request;
using Ecommerce.Dto.Response;
using Ecommerce.Dto.Wrapper;

namespace Ecommerce.Repository.ProductRepository
{
    public interface IProductRepository
    {
        Task<ResponseDto<string>> CreateProduct(ProductRequest request);

        Task<ResponseDto<string>> UpdateProduct(ProductRequest request, int productId);

        Task<ResponseDto<string>> DeleteProduct(int productId);

        Task<ResponseDto<List<ProductWrapper>>> FindAllProduct(int page, int size, string searchName, int? categoryId, int? brandId);

        Task<ResponseDto<ProductDetailWrapper>> FindProductById(int productId);

        Task<byte[]> ExportFileExcel(CancellationToken ct);


    }
}
