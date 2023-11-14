using Ecommerce.Dto.Request;
using Ecommerce.Dto.Response;

namespace Ecommerce.Service.RatingService
{
    public interface IRatingService
    {
        Task<ResponseDto<string>> CreateRatingToProduct(ProductRatingRequest request, int productId, string userId);

        Task<ResponseDto<string>> UpdateRatingToProduct(ProductRatingRequest request, int ratingId, string userId);

        Task<ResponseDto<string>> DeleteRatingToProduct(int ratingId, string userId);
    }
}
