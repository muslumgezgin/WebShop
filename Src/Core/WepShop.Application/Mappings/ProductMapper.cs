using AutoMapper;

namespace WepShop.Application.Mappings
{
    public class ProductMapper :Profile
    {
        public ProductMapper()
        {
            CreateMap<Domain.Entities.Product, Dtos.ProductDto>()
                .ReverseMap();
        }
        
    }
}