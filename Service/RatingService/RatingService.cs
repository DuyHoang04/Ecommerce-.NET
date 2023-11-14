using Ecommerce.Dto.Request;
using Ecommerce.Dto.Response;
using Ecommerce.Repository.RatingRepository;

namespace Ecommerce.Service.RatingService
{
    public class RatingService: IRatingService
    {
        private readonly IRatingRepository _ratingRepository;

        public RatingService(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        public async Task<ResponseDto<string>> CreateRatingToProduct(ProductRatingRequest request, int productId, string userId)
        {
            return await _ratingRepository.CreateRatingToProduct(request, productId, userId);
        }

        public async Task<ResponseDto<string>> DeleteRatingToProduct(int ratingId, string userId)
        {
            return await _ratingRepository.DeleteRatingToProduct(ratingId, userId);
        }

        public async Task<ResponseDto<string>> UpdateRatingToProduct(ProductRatingRequest request, int ratingId, string userId)
        {
            return await _ratingRepository.UpdateRatingToProduct(request, ratingId, userId);
        }
    }
}
