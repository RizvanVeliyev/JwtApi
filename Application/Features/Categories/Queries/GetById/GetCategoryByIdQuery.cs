using MediatR;

namespace Application.Features.Categories.Queries.GetById
{
    public class GetCategoryByIdQuery : IRequest<CategoryResponseDto>
    {
        public int Id { get; set; }
    }
}
