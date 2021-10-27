using MediatR;
using WepShop.Application.Wrappers;
using WepShop.Domain.Common;

namespace WepShop.Application.Features.Commands.BrandCommands
{
    public class DeleteBrandCommand :BaseEntity,IRequest<Response<Unit>>
    {
        public string BrandName { get; set; }
        public string Description { get; set; }
    }
}