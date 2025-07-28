using Domain.Entities;
using MediatR;
using Persistence.Repositories.Abstractions;

namespace Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CreateCategoryResponseDto>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CreateCategoryResponseDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Category
            {
                Name = request.Request.Name
            };

            await _categoryRepository.AddAsync(category);

            return new CreateCategoryResponseDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }
    }
}
