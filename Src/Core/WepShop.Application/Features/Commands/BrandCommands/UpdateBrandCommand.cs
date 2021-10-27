using MediatR;
using WepShop.Application.Wrappers;
using WepShop.Domain.Common;

namespace WepShop.Application.Features.Commands.BrandCommands
{
    public class UpdateBrandCommand :BaseEntity,IRequest<Response<Unit>>
    {
        public string BrandName { get; set; }
        public string Descripton { get; set; }
    }
}