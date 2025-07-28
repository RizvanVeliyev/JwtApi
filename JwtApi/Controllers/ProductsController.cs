using Application.Features.Products.Commands.CreateProduct;
using Application.Features.Products.Commands.DeleteProduct;
using Application.Features.Products.Commands.UpdateProduct;
using Application.Features.Products.Queries.GetAll;
using Application.Features.Products.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtApi.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateProductRequestDto dto)
        {
            var command = new CreateProductCommand { Request = dto };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update(UpdateProductRequestDto dto)
        {
            var command = new UpdateProductCommand { Request = dto };
            var result = await _mediator.Send(command);
            if (result == null)
                return NotFound("Product not found");

            return Ok(result);
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteProductCommand { Id = id };
            var result = await _mediator.Send(command);
            if (!result)
                return NotFound("Product not found");

            return Ok("Product deleted successfully");
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllProductsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }


        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetProductByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            if (result == null)
                return NotFound("Product not found");

            return Ok(result);
        }
    }
}
