using MediatR;
using WepShop.Application.Wrappers;
using WepShop.Domain.Common;

namespace WepShop.Application.Features.Commands.ProductCommands
{
    public class DeleteProductCommand :BaseEntity ,IRequest<Response>
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