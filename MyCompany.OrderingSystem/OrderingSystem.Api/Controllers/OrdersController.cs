using System.Threading.Tasks;
using AutoMapper;
using CustomerOrders;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderingSystem.DomainServices;
using OrderingSystem.WebApi.Validators;
// ReSharper disable MethodHasAsyncOverload

namespace OrderingSystem.WebApi.Controllers
{
    [ApiController, Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly SubmitOnlineOrderHandler _handler;
        private readonly OrderDtoValidator _validator;
        private readonly IMapper _mapper;

        public OrdersController(SubmitOnlineOrderHandler handler, OrderDtoValidator validator, IMapper mapper)
        {
            _handler = handler;
            _validator = validator;
            _mapper = mapper;
        }

        [HttpPost, Authorize]
        public async Task PostAsync(OrderDto dto)
        {
            _validator.ValidateAndThrow(dto);

            var command = new SubmitOrderCommand
            {
                CustomerName = User.Identity.Name!,
                CustomerEmail = User.GetEmail(),
                Positions = _mapper.Map(dto.Positions)
            };

            await _handler.HandleAsync(command);
        }
    }
}
