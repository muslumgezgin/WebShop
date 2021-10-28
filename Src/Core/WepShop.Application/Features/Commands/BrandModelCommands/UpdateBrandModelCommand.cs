using System;
using MediatR;
using WepShop.Application.Wrappers;
using WepShop.Domain.Common;

namespace WepShop.Application.Features.Commands.BrandModelCommands
{
    public class UpdateBrandModelCommand : BaseEntity ,IRequest<Response<Unit>>
    {
        public Guid BrandId { get; set; }
        public string ModelName { get; set; }
        public string Description { get; set; }
        
    }
}