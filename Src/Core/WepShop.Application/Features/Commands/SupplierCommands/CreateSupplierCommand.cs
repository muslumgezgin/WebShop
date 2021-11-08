using MediatR;
using WepShop.Application.Dtos;
using WepShop.Application.Wrappers;

namespace WepShop.Application.Features.Commands.SupplierCommands
{
    public class CreateSupplierCommand : IRequest<Response<SupplierDto>>
    {
        public string SupplierName { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }
        public string Logo { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
    }
}