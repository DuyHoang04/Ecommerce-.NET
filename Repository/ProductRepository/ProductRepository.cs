using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Ecommerce.Data;
using Ecommerce.Dto.Request;
using Ecommerce.Dto.Response;
using Ecommerce.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Dto.Wrapper;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Ecommerce.Helper;
using System.Reflection.Metadata.Ecma335;

namespace Ecommerce.Repository.ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _dbContext;
        private readonly Cloudinary _cloudinary;

        public ProductRepository(DataContext dbContext, Cloudinary cloudinary)
        {
            _dbContext = dbContext;
            _cloudinary = cloudinary;
        }

        public async Task<ResponseDto<string>> CreateProduct(ProductRequest request)
        {
            var _response = new ResponseDto<string>();

            try
            {
                var productExists = await _dbContext.Product.FirstOrDefaultAsync(p => p.Name == request.Name && p.IsDeleted == false);
                if (productExists != null)
                {
                    return _response.Response404("Product Already Exists");
                }

                var brand = await _dbContext.Brands.FirstOrDefaultAsync(b => b.BrandId == request.BrandId);
                if (brand == null)
                {
                    return _response.Response404("Brand Not Found");
                }

                var newProduct = new Product
                {
                    Name = request.Name,
                    Price = request.Price,
                    Stock = request.Stock,
                    Brand = brand,
                    Status = request.Status,
                };

                if (request.CategoryIds != null && request.CategoryIds.Any())
                {
                    foreach (var categoryId in request.CategoryIds)
                    {
                        var category = await _dbContext.Categories.FindAsync(categoryId);

                        if (category != null)
                        {
                            newProduct.Categories.Add(category);
                        }
                    }

                }

                _dbContext.Product.Add(newProduct);

                // Lưu thay đổi vào cơ sở dữ liệu
                await _dbContext.SaveChangesAsync();
                if (request.Images != null)
                {
                    var imageLinks = await UploadFile(request.Images, newProduct.ProductId);
                    if (imageLinks.Count == 0)
                    {
                        return _response.Response404("Error uploading images.");
                    }
                }
                return _response.ResponseSuccess("Create Product Successfully");
            }
            catch (Exception ex)
            {
                return _response.ResponseError(ex.Message);
            }
        }

 
   
        public async Task<ResponseDto<string>> DeleteProduct(int productId)
        {
            var _response = new ResponseDto<string>();

            try
            {
                var product = await _dbContext.Product.SingleOrDefaultAsync(x => x.ProductId == productId);
                if (product == null)
                {
                    return _response.Response404("Product Not Found");
                }
                product.IsDeleted = true;
                product.UpdatedAt = DateTime.Now;
                _dbContext.Product.Update(product);
                await _dbContext.SaveChangesAsync();
                return _response.ResponseSuccess("Delete Product Successfully");
            }
            catch (Exception ex)
            {
                return _response.ResponseError(ex.Message);
            }
        }

        public async Task<byte[]> ExportFileExcel(CancellationToken ct)
        {
            var products = await _dbContext
                .Product
                .Include(p => p.Categories)
                .Include(p => p.Brand)
                .Include(p => p.Images)
                .ToListAsync(ct);
            var productData = products.Select(item =>
            {
                var categories = string.Join(",", item.Categories.Select(c => c.Name));
                return new ProductToFileExcel
                {
                    ProductId = item.ProductId,
                    Name = item.Name,
                    Price = item.Price,
                    Stock = item.Stock,
                    Images = item.Images.First()?.OriginalLinkImage!,
                    BrandName = item.Brand.Name,
                    Categories = categories,
                    Status = item.Status,
                    IsDeleted = item.IsDeleted,
                    CreatedAt = item.CreatedAt,
                    UpdatedAt = item.UpdatedAt,
                };
            }).ToList();
            var file = ExcelHelper.CreateFile(productData);
            return file;
        }

        public async Task<ResponseDto<List<ProductWrapper>>> FindAllProduct(int page, int size, string searchName, int? categoryId, int? brandId)
        {
            var _response = new ResponseDto<List<ProductWrapper>>();
            try
            {

                var query = _dbContext.Product
                    .Include(p => p.Brand)
                    .Include(p => p.Categories)
                    .Include(p => p.Images)
                    .Where(p => p.IsDeleted == false);


                if (!string.IsNullOrWhiteSpace(searchName))
                {
                    query = query.Where(b => b.Name.Contains(searchName));
                }

                if (brandId.HasValue)
                {
                    query = query.Where(p => p.BrandId == brandId);
                }

                if (categoryId.HasValue)
                {
                    query = query.Where(p => p.Categories.Any(c => c.CategoryId == categoryId));
                }

                var productList = await query.Skip((page-1) * size).Take(size).ToListAsync();

                var productWrappers = productList.Select(product => new ProductWrapper
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    Price = product.Price,
                    Stock = product.Stock,
                    BrandId = product.BrandId,
                    BrandName = product.Brand.Name,
                    Categories = product.Categories.Select(category => new CategoryWrapper
                    {
                        CategoryId = category.CategoryId,
                        Name = category.Name,
                        Code = category.Code,
                        IsDeleted = category.IsDeleted,
                        CreatedAt = category.CreatedAt,
                        UpdatedAt = category.UpdatedAt,
                    }).ToList(),
                    Images = product.Images.Select(image => new ImageWrapper
                    {
                        ImageId = image.ImageId,
                        OriginalLinkImage = image.OriginalLinkImage,
                        IsDeleted = image.IsDeleted,
                        ProductId = image.ProductId
                    }).ToList(),
                    Status = product.Status,
                    IsDeleted = product.IsDeleted,
                    CreatedAt = product.CreatedAt,
                    UpdatedAt = product.UpdatedAt
                }).ToList();

                return _response.ResponseData(productWrappers);
            }
            catch (Exception ex)
            {
                return _response.ResponseError(ex.Message);
            }
        }

        public async Task<ResponseDto<ProductDetailWrapper>> FindProductById(int productId)
        {
            var _response = new ResponseDto<ProductDetailWrapper>();
            try
            {
                var product = await _dbContext.Product
                    .Include(p => p.Brand)
                    .Include(p => p.Categories)
                    .Include(p => p.Images)
                    .Include(p => p.Ratings)
                    .FirstOrDefaultAsync(p => p.ProductId == productId);

                if (product == null)
                {
                    return _response.NotFound("Product Not Found");
                }
                var productWrapper = new ProductDetailWrapper
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    Price = product.Price,
                    Stock = product.Stock,
                    BrandId = product.BrandId,
                    BrandName = product.Brand.Name,
                    Categories = product.Categories.Select(category => new CategoryWrapper
                    {
                        CategoryId = category.CategoryId,
                        Name = category.Name,
                        Code = category.Code,
                        IsDeleted = category.IsDeleted,
                        CreatedAt = category.CreatedAt,
                        UpdatedAt = category.UpdatedAt,
                    }).ToList(),
                    Ratings = product.Ratings.Select(rating => new RatingWrapper
                    {
                        RatingId = rating.RatingId,
                        Comment = rating.Comment,
                        StartPoint = rating.StartPoint,
                        UserName = rating.User.UserName!,
                        UserId = rating.UserId
                    }).ToList(),
                    Images = product.Images.Select(image => new ImageWrapper
                    {
                        ImageId = image.ImageId,
                        OriginalLinkImage = image.OriginalLinkImage,
                        IsDeleted = image.IsDeleted,
                        ProductId = image.ProductId
                    }).ToList(),
                    Status = product.Status,
                    IsDeleted = product.IsDeleted,
                    CreatedAt = product.CreatedAt,
                    UpdatedAt = product.UpdatedAt
                };
                
                return _response.ResponseData(productWrapper);


            }
            catch (Exception ex)
            {
                return _response.ResponseError(ex.Message);
            }
        }

        public async Task<ResponseDto<string>> UpdateProduct(ProductRequest request, int productId)
        {
            var _response = new ResponseDto<string>();
            try
            {
                var product = await _dbContext
                    .Product
                    .Include(p => p.Categories)
                    .Include(p => p.Images)
                    .FirstOrDefaultAsync(p => p.ProductId == productId);
                if (product == null)
                {
                    return _response.NotFound("Product Not Found");
                }
                product.Name = request.Name;
                product.Price = request.Price;
                product.Stock = request.Stock;
                product.BrandId = request.BrandId;
                product.Status = request.Status;
                product.UpdatedAt = DateTime.Now;
                product.Categories.Clear();

                if (request.CategoryIds != null && request.CategoryIds.Any())
                {
                    foreach (var categoryId in request.CategoryIds)
                    {
                        var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == categoryId);
                        if (category != null)
                        {
                            product.Categories.Add(category);
                        }
                    }
                }

                _dbContext.Product.Update(product);
                await _dbContext.SaveChangesAsync();

                if (request.Images != null)
                {
                    product.Images.Clear();
                    await _dbContext.SaveChangesAsync();
                    var imageLinks = await UploadFile(request.Images, productId);

                    if (imageLinks.Count == 0)
                    {
                        return _response.Response404("Error uploading images.");
                    }
                }

                return _response.ResponseSuccess("Update Product Successfully");
            }
            catch (Exception ex)
            {
                return _response.ResponseError(ex.Message);
            }
        }

        private async Task<List<string>> UploadFile(List<IFormFile> images, int productId)
        {
            var imageLinks = new List<string>();

            foreach (var imageFile in images)
            {
                var image = imageFile.OpenReadStream();
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(String.Format("{0}_{1}", "ecommerce", DateTime.Now), image),
                    UseFilename = true,
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
             
                if (!string.IsNullOrEmpty(uploadResult.SecureUrl?.ToString()))
                {
                    var originalLinkImageParam = new SqlParameter("@original_link_image", uploadResult.SecureUrl?.ToString());
                    var isDeletedParam = new SqlParameter("@is_deleted", false);
                    var productIdParam = new SqlParameter("@product_id", productId);


                     await _dbContext.Database.ExecuteSqlRawAsync("exec dbo.create_image @original_link_image, @is_deleted, @product_id", originalLinkImageParam, isDeletedParam, productIdParam);
                    
                    imageLinks.Add(uploadResult.SecureUrl!.ToString());
                }
            }

            return imageLinks;
        }
    }
}
