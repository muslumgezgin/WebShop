using AutoMapper;

namespace WepShop.Application.Mappings
{
    public class BrandModelMapper :Profile
    {
        public BrandModelMapper()
        {
            CreateMap<Domain.Entities.BrandModel, Dtos.BrandModelDto>()
                .ReverseMap();
        }
        
    }
}