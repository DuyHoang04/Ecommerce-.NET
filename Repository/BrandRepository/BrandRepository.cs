using AutoMapper;
using Azure;
using Azure.Core;
using Ecommerce.Data;
using Ecommerce.Dto.Request;
using Ecommerce.Dto.Response;
using Ecommerce.Dto.Wrapper;
using Ecommerce.Model;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository.BrandRepository
{
    public class BrandRepository : IBrandRepository
    {
        private readonly DataContext _dbContext;
        private readonly ILogger<BrandRepository> _logger;
        private readonly IMapper _mapper;

        public BrandRepository(
            DataContext dbContext,
            ILogger<BrandRepository> logger,
            IMapper mapper
            )
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ResponseDto<string>> CreateBrand(BrandRequest request)
        {
            var _response = new ResponseDto<string>();
            try
            {
                var brandExists = await _dbContext.Brands.FirstOrDefaultAsync(b => b.Name == request.Name && b.IsDeleted == false);
                if (brandExists != null)
                {
                    return _response.Response404("Brand Already Exist");
                }
                else
                {
                    var newBrand = new Brand
                    {
                        Name = request.Name,
                    };
                    _dbContext.Brands.Add(newBrand);
                    await _dbContext.SaveChangesAsync();
                    return _response.ResponseSuccess("Brand created successfully");
                }
            }
            catch (Exception ex)
            {
                return _response.ResponseError(ex.Message);
            }
        }

        public async Task<ResponseDto<string>> DeleteBrand(int brandId)
        {
            var _response = new ResponseDto<string>();
            try
            {
                var brand = await _dbContext.Brands.FirstOrDefaultAsync(b => b.BrandId == brandId);
                if (brand == null)
                {
                    return _response.NotFound("Brand Not Found");
                }
                brand.IsDeleted = true;
                brand.UpdatedAt = DateTime.Now;
                await _dbContext.SaveChangesAsync();

                return _response.ResponseSuccess("Delete Brand Successfully");
            } catch(Exception ex)
            {
                return _response.ResponseError(ex.Message);
            }
        }

        public async Task<ResponseDto<BrandWrapper>> FindBrandById(int brandId)
        {
            var _response = new ResponseDto<BrandWrapper>();
            try
            {
                var brand = await _dbContext.Brands.FirstOrDefaultAsync(b => b.BrandId == brandId);
                if(brand == null)
                {
                    return _response.NotFound("Brand Not Found");
                }
                var result = _mapper.Map<BrandWrapper>(brand);
                return _response.ResponseData(result);
            } catch(Exception ex)
            {
                return _response.ResponseError(ex.Message);

            }
        }

        public async Task<ResponseDto<List<BrandWrapper>>> GetAllBrand()
        {
            var _response = new ResponseDto<List<BrandWrapper>>();
            try
            {
                var brandList = await _dbContext.Brands.ToListAsync();
                var brandResult = _mapper.Map<List<BrandWrapper>>(brandList);
                return _response.ResponseData(brandResult);
            }
            catch (Exception ex)
            {
                return _response.ResponseError(ex.Message);
            }
        }

        public async Task<ResponseDto<string>> UpdateBrand(BrandRequest request, int brandId)
        {
            var _response = new ResponseDto<string>();
            try
            {
                var brand = await _dbContext.Brands.FirstOrDefaultAsync(b => b.BrandId == brandId);
                if(brand == null)
                {
                    return _response.NotFound("Brand Not Found");
                }
                brand.Name = request.Name;
                brand.UpdatedAt = DateTime.Now;
                await _dbContext.SaveChangesAsync();

                return _response.ResponseSuccess("Update Brand Successfully");
            } catch(Exception ex)
            {
                return _response.ResponseError(ex.Message);
            }
        }
    }
}
