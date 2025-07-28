using Domain.Entities;
using MediatR;
using Persistence.Repositories.Abstractions;

namespace Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductResponseDto>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<CreateProductResponseDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Request.Name,
                Price = request.Request.Price,
                CategoryId = request.Request.CategoryId
            };

            await _productRepository.AddAsync(product);

            return new CreateProductResponseDto
            {
                Id = product.Id,
                Name = product.Name
            };
        }
    }
}
