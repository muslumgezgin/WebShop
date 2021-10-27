using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WepShop.Application.Behaviours;
using WepShop.Application.Dtos;
using WepShop.Application.Interfaces.Repository.Base;
using WepShop.Application.Wrappers;
using WepShop.Domain.Entities;

namespace WepShop.Application.Features.Commands.BrandCommands
{
    public class PatchBrandCommandHandler:IRequestHandler<PatchBrandCommand, Response<Unit>>
    {
        readonly IMapper mapper;
        readonly IUnitOfWork unitOfWork;

        public PatchBrandCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Response<Unit>> Handle(PatchBrandCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new BadRequestException($"{nameof(PatchBrandCommand)} request is null");
            }

            Brand entity = await this.unitOfWork._brandRepository.GetByIdAsync(request.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Brand), request.Id);
            }

            BrandDto dto = this.mapper.Map<BrandDto>(entity);
            
            request.patchDto.ApplyTo(dto, request.modelState);

            if (!request.modelState.IsValid)
            {
                throw new BadRequestException($"{nameof(Brand)} Model path error");
            }

            entity = this.mapper.Map<Brand>(dto);
            await this.unitOfWork._brandRepository.UpdateAsync(entity);
            await this.unitOfWork.CommitAsync();

            return new Response<Unit>(Unit.Value);
        }
    }
}