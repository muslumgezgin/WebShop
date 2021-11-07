using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WepShop.Application.Behaviours;
using WepShop.Application.Exceptions;
using WepShop.Application.Interfaces.Repository.Base;
using WepShop.Application.Wrappers;
using WepShop.Domain.Entities;

namespace WepShop.Application.Features.Commands.BrandModelCommands
{
    public class UpdateBrandModelCommandHandler : IRequestHandler<UpdateBrandModelCommand,Response<Unit>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBrandModelCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<Unit>> Handle(UpdateBrandModelCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new BadRequestException($"{nameof(UpdateBrandModelCommand)} request is null");
            }
            
            BrandModel entity = await this._unitOfWork._brandModelRepository.GetByIdAsync(request.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(BrandModel), request.Id);
            }
            
            await this._unitOfWork._brandModelRepository.UpdateAsync(this._mapper.Map<BrandModel>(request));
            await this._unitOfWork.CommitAsync();

            return new Response<Unit>(Unit.Value);
        }
    }
}