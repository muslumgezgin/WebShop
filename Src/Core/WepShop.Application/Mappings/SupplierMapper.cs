using AutoMapper;

namespace WepShop.Application.Mappings
{
    public class SupplierMapper :Profile
    {
        public SupplierMapper()
        {
            CreateMap<Domain.Entities.Supplier, Dtos.ProductDto>()
                .ReverseMap();
        }
        
    }
}