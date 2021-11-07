using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WepShop.Application.Behaviours;
using WepShop.Application.Dtos;
using WepShop.Application.Exceptions;
using WepShop.Application.Interfaces.Repository.Base;
using WepShop.Application.Wrappers;
using WepShop.Domain.Entities;

namespace WepShop.Application.Features.Commands.BrandModelCommands
{
    public class PatchBrandModelCommandHandler :IRequestHandler<PatchBrandModelCommand,Response<Unit>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PatchBrandModelCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async  Task<Response<Unit>> Handle(PatchBrandModelCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new BadRequestException($"{nameof(PatchBrandModelCommand)} request is null");
            }
            BrandModel entity = await this._unitOfWork._brandModelRepository.GetByIdAsync(request.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(BrandModel), request.Id);
            }

            BrandModelDto dto = this._mapper.Map<BrandModelDto>(entity);
            request.patchDto.ApplyTo(dto,request.modelState);

            if (!request.modelState.IsValid)
            {
                throw new BadRequestException($"{nameof(BrandModel)} Model path error");
            }

            entity = this._mapper.Map<BrandModel>(dto);
            await this._unitOfWork._brandModelRepository.UpdateAsync(entity);
            await this._unitOfWork.CommitAsync();
            
            return new Response<Unit>(Unit.Value);
        }
    }
}