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

namespace WepShop.Application.Features.Commands.BrandCommands
{
    public class CreateBrandCommandHandler: IRequestHandler<CreateBrandCommand,Response<BrandDto>>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateBrandCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Response<BrandDto>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new BadRequestException($"{nameof(CreateBrandCommand)} request is null");
            }

            Brand entity = null;

            using (var transaction = await  this._unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    var newBrand = this.mapper.Map<Brand>(request);
                    entity = await this._unitOfWork._brandRepository.AddAsync(newBrand);

                    await this._unitOfWork.CommitAsync();

                    await transaction.CommitAsync(cancellationToken);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    transaction.RollbackAsync(cancellationToken);
                }
            }

            return new Response<BrandDto>(this.mapper.Map<BrandDto>(entity));
        }
    }
}