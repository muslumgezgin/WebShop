using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WepShop.Application.Exceptions;
using WepShop.Application.Interfaces.Repository.Base;
using WepShop.Application.Wrappers;
using WepShop.Domain.Entities;

namespace WepShop.Application.Features.Commands.SupplierCommands
{
    public class DeleteSupplierCommandHandler : IRequestHandler<DeleteSupplierCommand , Response>
    {
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public DeleteSupplierCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        public async  Task<Response> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
        {
            if (request == null || request.Id == Guid.Empty)
            {
                throw new BadRequestException($"{nameof(DeleteSupplierCommand)} request is null");
            }

            Supplier entity = await this._unitOfWork._supplierRepository.GetByIdAsync(request.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Supplier), request.Id);
            }

            await this._unitOfWork._supplierRepository.DeleteAsync(entity);
            await this._unitOfWork.CommitAsync();
            return new Response(true);
        }
    }
}