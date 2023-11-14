using Ecommerce.Data;
using Ecommerce.Dto.Request;
using Ecommerce.Dto.Response;
using Ecommerce.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository.RatingRepository
{
    public class RatingRepository : IRatingRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly DataContext _dbContext;

        public RatingRepository(DataContext dbContext, UserManager<ApplicationUser> userManager) {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async Task<ResponseDto<string>> CreateRatingToProduct(ProductRatingRequest request, int productId, string userId)
        {
            var _response = new ResponseDto<string>();
            try
            {
                var product = await _dbContext.Product.SingleOrDefaultAsync(p => p.ProductId == productId);
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return _response.NotFound("User Not Found");
                }
                if (product == null)
                {
                    return _response.NotFound("Product Not Found");
                }
                var rating = new Rating
                {
                    Comment = request.Comment,
                    StartPoint = request.StartPoint,
                    Product = product,
                    User = user
                    
                };
                 await _dbContext.Ratings.AddAsync(rating);
                await _dbContext.SaveChangesAsync();
                return _response.ResponseSuccess("Create Rating Successfully");
            }
            catch (Exception ex)
            {
                return _response.ResponseError(ex.Message);
            }
        }

        public async Task<ResponseDto<string>> DeleteRatingToProduct(int ratingId, string userId)
        {
            var _response = new ResponseDto<string>();
            try
            {
                var rating = await _dbContext.Ratings.FirstOrDefaultAsync(p => p.RatingId == ratingId);
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return _response.NotFound("User Not Found");
                }
                if (rating == null)
                {
                    return _response.NotFound("Rating Not Found");
                }
                _dbContext.Ratings.Remove(rating);
                await _dbContext.SaveChangesAsync();
                return _response.ResponseSuccess("Delete Rating Successfully");
            } catch(Exception ex)
            {
                return _response.ResponseError(ex.Message);
            }
        }

        public async Task<ResponseDto<string>> UpdateRatingToProduct(ProductRatingRequest request, int ratingId, string userId)
        {
            var _response = new ResponseDto<string>();
            try
            {
                var rating = await _dbContext.Ratings.FirstOrDefaultAsync(p => p.RatingId == ratingId);
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return _response.NotFound("User Not Found");
                }
                if (rating == null)
                {
                    return _response.NotFound("Rating Not Found");
                }
                rating.Comment = request.Comment;
                rating.StartPoint = request.StartPoint;
                rating.UpdatedAt = DateTime.Now;

                _dbContext.Ratings.Update(rating);
                await _dbContext.SaveChangesAsync();
                return _response.ResponseSuccess("Update Rating Successfully");
            } catch(Exception ex)
            {
                return _response.ResponseError(ex.Message);
            }
        }
    }
}
