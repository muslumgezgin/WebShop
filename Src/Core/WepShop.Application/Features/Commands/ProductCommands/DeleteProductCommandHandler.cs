using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Server.IIS.Core;
using WepShop.Application.Exceptions;
using WepShop.Application.Interfaces.Repository.Base;
using WepShop.Application.Wrappers;
using WepShop.Domain.Common;
using WepShop.Domain.Entities;

namespace WepShop.Application.Features.Commands.ProductCommands
{
    public class DeleteProductCommandHandler :IRequestHandler<DeleteProductCommand,Response>
    {
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public DeleteProductCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            if (request == null || request.Id == Guid.Empty)
            {
                throw new BadRequestException($"{nameof(DeleteProductCommand)} request is null");
            }

            Product entity = await this._unitOfWork._productRepository.GetByIdAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            await this._unitOfWork._productRepository.DeleteAsync(entity);
            await this._unitOfWork.CommitAsync();
            return new Response(true);
        }
    }
}