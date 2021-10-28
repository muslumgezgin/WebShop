using AutoMapper;
using WepShop.Application.Features.Commands.BrandCommands;

namespace WepShop.Application.Mappings
{
    public class BrandMapper : Profile
    {
        public BrandMapper()
        {
            CreateMap<Domain.Entities.Brand, Dtos.BrandDto>().ReverseMap();
            CreateMap<Domain.Entities.Brand, CreateBrandCommand>()
                .ReverseMap();
            CreateMap<Domain.Entities.Brand, UpdateBrandCommand>()
                .ReverseMap();
            CreateMap<Domain.Entities.Brand, DeleteBrandCommand>()
                .ReverseMap();
            
        }
        
    }
}