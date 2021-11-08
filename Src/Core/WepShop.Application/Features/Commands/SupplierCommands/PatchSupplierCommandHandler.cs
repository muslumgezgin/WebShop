using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WepShop.Application.Dtos;
using WepShop.Application.Exceptions;
using WepShop.Application.Interfaces.Repository.Base;
using WepShop.Application.Wrappers;
using WepShop.Domain.Entities;

namespace WepShop.Application.Features.Commands.SupplierCommands
{
    public class PatchSupplierCommandHandler : IRequestHandler<PatchSupplierCommand,Response>
    {
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public PatchSupplierCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response> Handle(PatchSupplierCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new BadRequestException($"{nameof(PatchSupplierCommand)} request is null");
            }

            Supplier entity = await this._unitOfWork._supplierRepository.GetByIdAsync(request.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Supplier), request.Id);
            }
            SupplierDto dto = this._mapper.Map<SupplierDto>(entity);
            request.patchDto.ApplyTo(dto, request.modelState);
            if (!request.modelState.IsValid)
            {
                throw new BadRequestException($"{nameof(Supplier)} Model path error");
            }

            entity = this._mapper.Map<Supplier>(dto);
            await this._unitOfWork._supplierRepository.UpdateAsync(entity);
            await this._unitOfWork.CommitAsync();
            return new Response(true);
        }
    }
}