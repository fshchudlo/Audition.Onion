using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using CustomerOrders;
using OrderingSystem.WebApi.Controllers;

namespace OrderingSystem.WebApi
{
    public static class TypeExtensions
    {
        public static string GetEmail(this ClaimsPrincipal principal)
        {
            return principal.Claims.Single(c => c.Type == ClaimTypes.Email).Value;
        }
        public static IReadOnlyCollection<ProductQuantity> Map(this IMapper mapper, IEnumerable<OrderPositionDto> positions)
        {
            return mapper.Map<IReadOnlyCollection<ProductQuantity>>(positions);
        }
    }
}
