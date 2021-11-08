using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WepShop.Application.Dtos;
using WepShop.Application.Exceptions;
using WepShop.Application.Interfaces.Repository.Base;
using WepShop.Application.Wrappers;
using WepShop.Domain.Entities;

namespace WepShop.Application.Features.Commands.ProductCommands
{
    public class CreateProductCommandHandler :IRequestHandler<CreateProductCommand,Response<ProductDto>>
    {
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public CreateProductCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<ProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {

            if (request == null)
            {
                throw new BadRequestException($"{nameof(CreateProductCommand)} request is null");
            }

            Product entity = null;

            using (var transaction = await  this._unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    var productMapper = this._mapper.Map<Product>(request);
                    entity = await this._unitOfWork._productRepository.AddAsync(productMapper);
                    await this._unitOfWork.CommitAsync();
                    await transaction.CommitAsync(cancellationToken);
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw e;
                }

            }

            var productDto = this._mapper.Map<ProductDto>(entity);
            return new Response<ProductDto>(productDto);
        }
    }
}