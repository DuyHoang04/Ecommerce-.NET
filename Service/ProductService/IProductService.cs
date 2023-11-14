using Ecommerce.Dto.Request;
using Ecommerce.Dto.Response;
using Ecommerce.Dto.Wrapper;
using Ecommerce.Model;

namespace Ecommerce.Service.ProductService
{
    public interface IProductService
    {
        Task<ResponseDto<string>> CreateProduct(ProductRequest request);

        Task<ResponseDto<string>> UpdateProduct(ProductRequest request, int productId);

        Task<ResponseDto<string>> DeleteProduct(int productId);

        Task<ResponseDto<List<ProductWrapper>>> FindAllProduct(int page, int size, string searchName, int? categoryId, int? brandId);

        Task<ResponseDto<ProductDetailWrapper>> FindProductById(int productId);

        Task<byte[]> ExportFileExcel(CancellationToken ct);



    }
}
