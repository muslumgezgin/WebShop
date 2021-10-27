using AutoMapper;

namespace WepShop.Application.Mappings
{
    public class BrandMapper : Profile
    {
        public BrandMapper()
        {
            CreateMap<Domain.Entities.Brand, Dtos.BrandDto>().ReverseMap();
            
        }
        
    }
}