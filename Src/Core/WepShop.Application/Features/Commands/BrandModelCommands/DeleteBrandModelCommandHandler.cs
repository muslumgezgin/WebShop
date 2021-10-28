using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WepShop.Application.Behaviours;
using WepShop.Application.Dtos;
using WepShop.Application.Interfaces.Repository.Base;
using WepShop.Application.Wrappers;
using WepShop.Domain.Entities;

namespace WepShop.Application.Features.Commands.BrandModelCommands
{
    public class DeleteBrandModelCommandHandler : IRequestHandler<DeleteBrandModelCommand,Response<Unit>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBrandModelCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
        }
        
        public async Task<Response<Unit>> Handle(DeleteBrandModelCommand request, CancellationToken cancellationToken)
        {
            if (request == null || request.Id == Guid.Empty)
            {
                throw new BadRequestException($"{nameof(DeleteBrandModelCommand)} request is null");
            }

            BrandModel entity = await this._unitOfWork._brandModelRepository.GetByIdAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(BrandModel), request.Id);
            }

            await this._unitOfWork._brandModelRepository.DeleteAsync(entity);
            await this._unitOfWork.CommitAsync();
            return new Response<Unit>(Unit.Value);
        }
    }
}