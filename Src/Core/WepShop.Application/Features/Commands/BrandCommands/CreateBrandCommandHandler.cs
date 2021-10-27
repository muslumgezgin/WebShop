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
        private IMapper mapper;
        private IUnitOfWork unitOfWork;

        public CreateBrandCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Response<BrandDto>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new BadRequestException($"{nameof(CreateBrandCommand)} request is null");
            }

            Brand entity = null;

            using (var transaction = await  this.unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    var newBrand = this.mapper.Map<Brand>(request);
                    entity = await this.unitOfWork._brandRepository.AddAsync(newBrand);

                    await this.unitOfWork.CommitAsync();

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