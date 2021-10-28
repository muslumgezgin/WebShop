using AutoMapper;
using WepShop.Application.Features.Commands.BrandModelCommands;

namespace WepShop.Application.Mappings
{
    public class BrandModelMapper :Profile
    {
        public BrandModelMapper()
        {
            CreateMap<Domain.Entities.BrandModel, Dtos.BrandModelDto>()
                .ReverseMap();
            CreateMap<Domain.Entities.BrandModel, CreateBrandModelCommand>()
                .ReverseMap();
            CreateMap<Domain.Entities.BrandModel, UpdateBrandModelCommand>()
                .ReverseMap();
            CreateMap<Domain.Entities.BrandModel, DeleteBrandModelCommand>()
                .ReverseMap();
        }
        
    }
}