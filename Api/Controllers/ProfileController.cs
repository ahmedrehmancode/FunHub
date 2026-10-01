using Application.Feature.Profile.Commands.UpdateProfile;
using Application.Feature.Profile.Query.GetMyProfile;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  
    public class ProfileController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private string CurrentUserId =>
            User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

        // GET api/profile/me
        [HttpGet("me")]
        public async Task<IActionResult> GetMyProfile()
        {
            var result = await _mediator.Send(new GetMyProfileQuery { UserId = CurrentUserId });
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        // PUT api/profile/me
        [HttpPut("me")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateMyProfile([FromForm] UpdateProfileRequestDTO request)
        {
            UpdateProfileCommand command = new UpdateProfileCommand
            {
                UserId = CurrentUserId,
                FullName = request.FullName,
                Bio = request.Bio,
                Username = request.Username
            };
            command.UserId = CurrentUserId;   // token se, client se nahi
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }

}
