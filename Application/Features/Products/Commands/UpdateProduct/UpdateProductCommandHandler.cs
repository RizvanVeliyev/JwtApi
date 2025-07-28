using MediatR;
using Persistence.Repositories.Abstractions;

namespace Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdateProductResponseDto>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<UpdateProductResponseDto?> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var existing = await _productRepository.GetByIdAsync(request.Request.Id);
            if (existing == null)
                return null;

            existing.Name = request.Request.Name;
            existing.Price = request.Request.Price;
            existing.CategoryId = request.Request.CategoryId;

            await _productRepository.UpdateAsync(existing);

            return new UpdateProductResponseDto
            {
                Id = existing.Id,
                Name = existing.Name
            };
        }
    }
}
