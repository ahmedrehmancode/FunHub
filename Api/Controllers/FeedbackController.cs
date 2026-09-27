using Application.Common;
using Application.Feature.Feedback.Commands.SubmitFeedback;
using Application.Feature.Feedback.Queries.GetAllFeedback;
using Application.Feature.Feedback.Queries.GetMyFeedback;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FeedbackController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Submit(SubmitFeedbackCommand command)
        {
            var CurrentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            command.UserId = CurrentUserId!;
            var result = await _mediator.Send(command);
            return Ok(ApiResponse<object>.SuccessResponse(result.Data));
        }

        [HttpGet("mine")]
        [Authorize]
        public async Task<IActionResult> GetMine()
        {
            var CurrentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _mediator.Send(new GetMyFeedbackQuery { UserId = CurrentUserId! });
            return Ok(ApiResponse<object>.SuccessResponse(result.Data));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllFeedbackQuery());
            return Ok(ApiResponse<object>.SuccessResponse(result.Data));
        }
    }
}
