using Application.Features.Categories.Commands.CreateCategory;
using Application.Features.Categories.Commands.DeleteCategory;
using Application.Features.Categories.Commands.UpdateCategory;
using Application.Features.Categories.Queries.GetAll;
using Application.Features.Categories.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence.Repositories.Abstractions;

namespace JwtApi.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(IMediator mediator, ICategoryRepository categoryRepository)
        {
            _mediator = mediator;
            _categoryRepository = categoryRepository;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateCategoryRequestDto dto)
        {
            var command = new CreateCategoryCommand { Request = dto };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllCategoriesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] UpdateCategoryRequestDto dto)
        {
            var command = new UpdateCategoryCommand { Request = dto };
            var result = await _mediator.Send(command);
            if (!result) return NotFound("Category not found.");
            return Ok("Category updated successfully.");
        }

        //[HttpDelete("[action]/{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var command = new DeleteCategoryCommand { Id = id };
        //    var result = await _mediator.Send(command);
        //    if (!result) return NotFound("Category not found.");
        //    return Ok("Category deleted successfully.");
        //}


        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetCategoryByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            if (result == null)
                return NotFound("Category not found");

            return Ok(result);
        }
    }
}
