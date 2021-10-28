using System;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WepShop.Application.Dtos;
using WepShop.Application.Wrappers;
using WepShop.Domain.Common;

namespace WepShop.Application.Features.Commands.BrandModelCommands
{
    public class PatchBrandModelCommand :BaseEntity,IRequest<Response<Unit>>
    {

        public JsonPatchDocument<BrandModelDto> patchDto { get; set; }

        public ModelStateDictionary modelState { get; }

        public PatchBrandModelCommand(Guid id,JsonPatchDocument<BrandModelDto> patchDto, ModelStateDictionary modelState)
        {
            this.Id = id;
            this.patchDto = patchDto;
            this.modelState = modelState;
        }
    }
}