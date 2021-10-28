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
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PatchBrandCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
        }
        public async Task<Response<Unit>> Handle(PatchBrandCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new BadRequestException($"{nameof(PatchBrandCommand)} request is null");
            }

            Brand entity = await this._unitOfWork._brandRepository.GetByIdAsync(request.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Brand), request.Id);
            }

            BrandDto dto = this._mapper.Map<BrandDto>(entity);
            
            request.patchDto.ApplyTo(dto, request.modelState);

            if (!request.modelState.IsValid)
            {
                throw new BadRequestException($"{nameof(Brand)} Model path error");
            }

            entity = this._mapper.Map<Brand>(dto);
            await this._unitOfWork._brandRepository.UpdateAsync(entity);
            await this._unitOfWork.CommitAsync();

            return new Response<Unit>(Unit.Value);
        }
    }
}