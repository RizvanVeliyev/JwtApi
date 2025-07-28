using Application.Features.Auth.Commands.ChangePassword;
using Application.Features.Auth.Commands.CreateRefreshToken;
using Application.Features.Auth.Commands.Delete;
using Application.Features.Auth.Commands.ForgotPassword;
using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Commands.Logout;
using Application.Features.Auth.Commands.Register;
using Application.Features.Auth.Commands.ResetPassword;
using Application.Features.Auth.Commands.Update;
using Application.Features.Auth.Queries.GetAllUsers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto requestDto)
        {
            var command = new RegisterCommand { Request = requestDto };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto requestDto)
        {
            var command = new LoginCommand { Request = requestDto };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Logout([FromBody] LogoutRequestDto dto)
        {
            var command = new LogoutCommand { RefreshToken = dto.RefreshToken };
            var result = await _mediator.Send(command);

            if (!result) return BadRequest("Logout failed");
            return Ok("Logged out successfully");
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDto dto)
        {
            var command = new ForgotPasswordCommand { Email = dto.Email };
            var link = await _mediator.Send(command);

            if (link == null) return NotFound("User not found");
            return Ok(new { ResetLink = link });
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto dto)
        {
            var command = new ResetPasswordCommand
            {
                Token = dto.Token,
                NewPassword = dto.NewPassword,
                ConfirmPassword = dto.ConfirmPassword
            };
            var result = await _mediator.Send(command);

            if (!result) return BadRequest("Invalid token or passwords do not match");
            return Ok("Password successfully changed");
        }
    }

}
