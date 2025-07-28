using MediatR;

namespace Application.Features.Categories.Queries.GetAll
{
    public class GetAllCategoriesQuery : IRequest<List<CategoryDto>> { }

}
