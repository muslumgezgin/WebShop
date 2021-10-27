using System;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WepShop.Application.Dtos;
using WepShop.Application.Wrappers;
using WepShop.Domain.Common;

namespace WepShop.Application.Features.Commands.BrandCommands
{
    public class PatchBrandCommand : BaseEntity ,IRequest<Response<Unit>>
    {
        public JsonPatchDocument<BrandDto>  patchDto { get; set; }
        public ModelStateDictionary modelState { get; }

        public PatchBrandCommand(Guid id,JsonPatchDocument<BrandDto> patchDto, ModelStateDictionary modelState)
        {
            this.Id = id;
            this.patchDto = patchDto;
            this.modelState = modelState;
        }
    }
}