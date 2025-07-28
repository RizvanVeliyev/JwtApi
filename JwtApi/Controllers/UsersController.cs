using Application.Features.Auth.Commands.ChangePassword;
using Application.Features.Auth.Commands.Delete;
using Application.Features.Auth.Commands.Update;
using Application.Features.Auth.Queries.GetAllUsers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JwtApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("[action]")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _mediator.Send(new GetAllUsersQuery());
            return Ok(result);
        }

        [HttpDelete("[action]/{id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var command = new DeleteUserCommand { UserId = id };
            await _mediator.Send(command);
            return NoContent();
        }


        [Authorize]
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteYourProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var command = new DeleteUserCommand { UserId = userId };
            await _mediator.Send(command);
            return Ok("Your profile has been deleted successfully");
        }



        [HttpPut("[action]")]
        [Authorize]
        public async Task<ActionResult<UpdateUserResponseDto>> UpdateProfile(UpdateUserRequestDto dto)
        {
            var command = new UpdateUserCommand { Request = dto };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("[action]")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequestDto dto)
        {
            var command = new ChangePasswordCommand { Request = dto };
            var result = await _mediator.Send(command);

            if (!result) return BadRequest("Password not changed");
            return Ok("Password updated successfully");
        }
    }

}
