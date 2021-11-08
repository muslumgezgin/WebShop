using System;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WepShop.Application.Dtos;
using WepShop.Application.Wrappers;
using WepShop.Domain.Common;

namespace WepShop.Application.Features.Commands.SupplierCommands
{
    public class PatchSupplierCommand : BaseEntity,IRequest<Response>
    {
        public JsonPatchDocument<SupplierDto> patchDto { get; set; }
        public ModelStateDictionary modelState { get; }

        public PatchSupplierCommand(Guid id, JsonPatchDocument<SupplierDto> patchDto, ModelStateDictionary modelState)
        {
            this.Id = id;
            this.patchDto = patchDto;
            this.modelState = modelState;
        }
        
    }
}