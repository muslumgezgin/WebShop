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

namespace WepShop.Application.Features.Commands.SupplierCommands
{
    public class CreateSupplierCommandHandler : IRequestHandler<CreateSupplierCommand,Response<SupplierDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateSupplierCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        
        public async  Task<Response<SupplierDto>> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
            
            if (request == null)
            {
                throw new BadRequestException($"{nameof(CreateSupplierCommand)} request is null");
            }

            Supplier entity = null;

            using (var transaction = await this._unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    var newEntity = this._mapper.Map<Supplier>(request);
                    entity = await this._unitOfWork._supplierRepository.AddAsync(newEntity);

                    await this._unitOfWork.CommitAsync();
                    await transaction.CommitAsync(cancellationToken);

                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw e;
                }
     
            }

            var dto = this._mapper.Map<SupplierDto>(entity);
            return new Response<SupplierDto>(dto);
        }
    }
}