using MediatR;

namespace Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<CreateCategoryResponseDto>
    {
        public CreateCategoryRequestDto Request { get; set; }
    }
}
