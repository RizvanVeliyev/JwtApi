using MediatR;

namespace Application.Features.Products.Commands.CreateProduct
{

    public class CreateProductCommand : IRequest<CreateProductResponseDto>
    {
        public CreateProductRequestDto Request { get; set; }
    }
}
