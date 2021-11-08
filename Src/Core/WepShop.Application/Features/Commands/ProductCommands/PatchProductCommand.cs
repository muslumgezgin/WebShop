using System;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WepShop.Application.Dtos;
using WepShop.Application.Wrappers;
using WepShop.Domain.Common;

namespace WepShop.Application.Features.Commands.ProductCommands
{
    public class PatchProductCommand :BaseEntity, IRequest<Response>
    {
        public JsonPatchDocument<ProductDto>  patchDto { get; set; }
        public ModelStateDictionary modelState { get; set; }

        public PatchProductCommand(Guid id,JsonPatchDocument<ProductDto> patchDto, ModelStateDictionary modelState)
        {
            this.Id = id;
            this.patchDto = patchDto;
            this.modelState = modelState;
        }
    }
}