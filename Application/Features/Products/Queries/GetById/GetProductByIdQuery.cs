using Application.Features.Products.Queries.GetAll;
using MediatR;

namespace Application.Features.Products.Queries.GetById
{
    public class GetProductByIdQuery : IRequest<ProductResponseDto?>
    {
        public int Id { get; set; }
    }
}
