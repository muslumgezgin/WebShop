using System;
using MediatR;
using WepShop.Application.Dtos;
using WepShop.Application.Wrappers;

namespace WepShop.Application.Features.Commands.BrandModelCommands
{
    public class CreateBrandModelCommand :IRequest<Response<BrandModelDto>>
    {
        public Guid BrandId { get; set; }
        public string ModelName { get; set; }
        public string Description { get; set; }
    }
}