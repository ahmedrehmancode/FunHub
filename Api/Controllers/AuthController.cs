using Application.Common;
using Application.Feature.Auth.Commands.ForgotPassword;
using Application.Feature.Auth.Commands.Login;
using Application.Feature.Auth.Commands.Register;
using Application.Feature.Auth.Commands.ResetPassword;
using Application.Feature.Auth.Commands.VerifyEmail;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IMediator mediator, ILogger<AuthController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCommand cmd)
        {
            var result = await _mediator.Send(cmd);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse<object>.ValidationResponse(result.Errors));

            return Ok(ApiResponse<object>.SuccessResponse(result.Data));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand cmd)
        {
            var result = await _mediator.Send(cmd);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse<object>.ValidationResponse(result.Errors));

            return Ok(ApiResponse<object>.SuccessResponse(result.Data));
        }


        // Api/Controllers/AuthController.cs
        [HttpGet("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromQuery] string token, [FromQuery] string email)
        {
            var result = await _mediator.Send(new VerifyEmailCommand { Token = token, Email = email });
            // return result.IsSuccess ? Ok(result) : BadRequest(result.Errors);
            if (result.IsSuccess)
            {
                return Redirect("https://fanhubplus.net/login");
            }

            return Redirect("https://fanhubplus.net/email-verification-failed");
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand command)
        {
            _logger.LogInformation("Sending forgot password request for email: {Email}", command.Email);
            var result = await _mediator.Send(command);
            return Ok(new { message = "If this email exists, a reset link has been sent." });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return BadRequest(ApiResponse<object>.ValidationResponse(result.Errors));

            return Ok(ApiResponse<object>.SuccessResponse(result.Data));
        }
    }
}
