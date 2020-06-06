using FluentValidation;
using OrderingSystem.WebApi.Controllers;

namespace OrderingSystem.WebApi.Validators
{
    public class OrderDtoValidator: AbstractValidator<OrderDto>
    {
        public OrderDtoValidator()
        {
            RuleFor(x => x.Positions)
                .NotEmpty();
        }
    }
}
