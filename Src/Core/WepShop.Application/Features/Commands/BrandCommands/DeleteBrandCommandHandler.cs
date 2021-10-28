using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WepShop.Application.Behaviours;
using WepShop.Application.Interfaces.Repository.Base;
using WepShop.Application.Wrappers;
using WepShop.Domain.Entities;

namespace WepShop.Application.Features.Commands.BrandCommands
{
    public class DeleteBrandCommandHandler:IRequestHandler<DeleteBrandCommand,Response<Unit>>
    {
        private readonly  IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBrandCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
        }
        
        public async  Task<Response<Unit>> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            if (request == null || request.Id == Guid.Empty)
            {
                throw new BadRequestException($"{nameof(DeleteBrandCommand)} request is null");
            }

            Brand entity = await this._unitOfWork._brandRepository.GetByIdAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Brand), request.Id);
            }

            await this._unitOfWork._brandRepository.DeleteAsync(entity);
            await this._unitOfWork.CommitAsync();

            return new Response<Unit>(Unit.Value);
        }
    }
}