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
    public class UpdateSupplierCommandHandler : IRequestHandler<UpdateSupplierCommand,Response>
    {
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public UpdateSupplierCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async  Task<Response> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new BadRequestException($"{nameof(UpdateSupplierCommand)} request is null");
            }

            Supplier entity = await this._unitOfWork._supplierRepository.GetByIdAsync(request.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Supplier), request.Id);
            }

            var mapEntity = this._mapper.Map<Supplier>(request);
            await this._unitOfWork._supplierRepository.UpdateAsync(mapEntity);
            await this._unitOfWork.CommitAsync();
            return new Response(true);

        }
    }
}