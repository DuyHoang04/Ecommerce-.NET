using AutoMapper;
using Ecommerce.Dto.Wrapper;
using Ecommerce.Model;

namespace Ecommerce.Config
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            // Ánh xạ từ Model sang DTO
          

            // Ánh xạ từ DTO sang Model
            CreateMap<Brand, BrandWrapper>();
            CreateMap<Category, CategoryWrapper>();

        }
    }
}
