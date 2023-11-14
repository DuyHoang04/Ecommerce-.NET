using Ecommerce.Dto.Request;
using Ecommerce.Dto.Response;
using Ecommerce.Dto.Wrapper;
using Ecommerce.Model;

namespace Ecommerce.Repository.BrandRepository
{
    public interface IBrandRepository
    {
        Task<ResponseDto<string>> CreateBrand(BrandRequest request);
        Task<ResponseDto<List<BrandWrapper>>> GetAllBrand();

        Task<ResponseDto<string>> UpdateBrand(BrandRequest request, int brandId);

        Task<ResponseDto<string>> DeleteBrand(int brandId);

        Task<ResponseDto<BrandWrapper>> FindBrandById(int brandId);




    }
}
