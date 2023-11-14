using Ecommerce.Dto.Request;
using Ecommerce.Dto.Response;
using Ecommerce.Dto.Wrapper;
using Ecommerce.Model;
using Ecommerce.Repository.BrandRepository;

namespace Ecommerce.Service.BrandService
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;

        public BrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task<ResponseDto<string>> CreateBrand(BrandRequest request)
        {
          return await _brandRepository.CreateBrand(request);
        }

        public async Task<ResponseDto<string>> DeleteBrand(int brandId)
        {
            return await _brandRepository.DeleteBrand(brandId);
        }

        public async Task<ResponseDto<BrandWrapper>> FindBrandById(int brandId)
        {
            return await _brandRepository.FindBrandById(brandId);
        }

        public async Task<ResponseDto<List<BrandWrapper>>> GetAllBrand()
        {
            return await _brandRepository.GetAllBrand();
        }

        public async Task<ResponseDto<string>> UpdateBrand(BrandRequest request, int brandId)
        {
            return await _brandRepository.UpdateBrand(request, brandId);
        }
    }
}
