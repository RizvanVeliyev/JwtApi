using MediatR;

namespace Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest<bool>
    {
        public UpdateCategoryRequestDto Request { get; set; }
    }
}
