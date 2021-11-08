using MediatR;
using WepShop.Application.Dtos;
using WepShop.Application.Wrappers;

namespace WepShop.Application.Features.Commands.ProductCommands
{
    public class CreateProductCommand : IRequest<Response<ProductDto>>
    {
        public string SupplierId { get; set; }
        
        public string BrandModelId { get; set; }
        
        public string ProductCode { get; set; }
        
        public string ProductName { get; set; }
        
        public string Description { get; set; }
        
        public double Price { get; set; }
        
        public int Quantity { get; set; }

    }
}