using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WepShop.Application.Behaviours;
using WepShop.Application.Dtos;
using WepShop.Application.Exceptions;
using WepShop.Application.Interfaces.Repository.Base;
using WepShop.Application.Wrappers;
using WepShop.Domain.Entities;

namespace WepShop.Application.Features.Commands.BrandModelCommands
{
    public class CreateBrandModelCommandHandler : IRequestHandler<CreateBrandModelCommand,Response<BrandModelDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateBrandModelCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
        }
        public async Task<Response<BrandModelDto>> Handle(CreateBrandModelCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new BadRequestException($"{nameof(CreateBrandModelCommand)} is request empty");
            }

            BrandModel entity = null;

            using (var transcation = await  this._unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    var newBrandModel = this._mapper.Map<BrandModel>(request);
                    entity = await this._unitOfWork._brandModelRepository.AddAsync(newBrandModel);
                    await this._unitOfWork.CommitAsync();
                    await transcation.CommitAsync(cancellationToken);
                }
                catch(Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    await transcation.RollbackAsync(cancellationToken);
                } 
            }

            return new Response<BrandModelDto>(this._mapper.Map<BrandModelDto>(entity));
        }
    }
}