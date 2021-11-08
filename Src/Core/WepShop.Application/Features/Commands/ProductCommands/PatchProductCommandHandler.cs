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

namespace WepShop.Application.Features.Commands.ProductCommands
{
    public class PatchProductCommandHandler : IRequestHandler<PatchProductCommand , Response>
    {
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;


        public async Task<Response> Handle(PatchProductCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new BadRequestException($"{nameof(PatchProductCommand)} request is null");
            }

            Product entiity = await this._unitOfWork._productRepository.GetByIdAsync(request.Id);

            if (entiity == null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            ProductDto dto = this._mapper.Map<ProductDto>(entiity);
            request.patchDto.ApplyTo(dto, request.modelState);

            if (!request.modelState.IsValid)
            {
                throw new BadRequestException($"{nameof(Product)} Model path error");
            }

            entiity = this._mapper.Map<Product>(dto);
            await this._unitOfWork._productRepository.UpdateAsync(entiity);
            await this._unitOfWork.CommitAsync();
            return new Response(true);

        }
    }
}