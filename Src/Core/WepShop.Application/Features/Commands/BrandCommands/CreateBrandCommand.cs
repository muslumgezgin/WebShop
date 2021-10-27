using MediatR;
using WepShop.Application.Dtos;
using WepShop.Application.Wrappers;

namespace WepShop.Application.Features.Commands.BrandCommands
{
    public class CreateBrandCommand : IRequest<Response<BrandDto>>
    {
        public string BrandName { get; set; }
        public string Descriptiom { get; set; }
    }
}