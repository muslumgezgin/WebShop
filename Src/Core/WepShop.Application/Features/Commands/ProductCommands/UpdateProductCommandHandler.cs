using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WepShop.Application.Exceptions;
using WepShop.Application.Interfaces.Repository.Base;
using WepShop.Application.Wrappers;
using WepShop.Domain.Entities;

namespace WepShop.Application.Features.Commands.ProductCommands
{
    public class UpdateProductCommandHandler :IRequestHandler<UpdateProductCommand,Response>
    {
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public UpdateProductCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async  Task<Response> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new BadRequestException($"{nameof(UpdateProductCommand)} request is null");
            }

            Product entity = await this._unitOfWork._productRepository.GetByIdAsync(request.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }
            
            await this._unitOfWork._productRepository.UpdateAsync(this._mapper.Map<Product>(request));
            await this._unitOfWork.CommitAsync();

            return new Response(true);
        }
    }
}