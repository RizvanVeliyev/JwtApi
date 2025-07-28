using Application.Features.Products.Queries.GetAll;
using MediatR;
using Persistence.Repositories.Abstractions;

namespace Application.Features.Products.Queries.GetById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductResponseDto?>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductResponseDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var p = await _productRepository.GetByIdWithCategoryAsync(request.Id);
            if (p == null)
                return null;

            return new ProductResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                CategoryId = p.CategoryId,
                CategoryName = p.Category?.Name ?? ""
            };
        }
    }
}
