using MediatR;

namespace Application.Features.Products.Queries.GetAll
{
    public class GetAllProductsQuery : IRequest<List<ProductResponseDto>>
    {
    }
}
