using MediatR;

namespace Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<UpdateProductResponseDto>
    {
        public UpdateProductRequestDto Request { get; set; } = null!;
    }
}
