using Application.Common;
using Application.Feature.Bookmarks.Commands.AddBookmark;
using Application.Feature.Bookmarks.Commands.RemoveBookmark;
using Application.Feature.Bookmarks.Queries.GetMyBookmarks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookmarksController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BookmarksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{contentId}")]
        public async Task<IActionResult> Add(int contentId)
        {
            var CurrentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var command = new AddBookmarkCommand { UserId = CurrentUserId!, ContentId = contentId };
            var result = await _mediator.Send(command);
            return Ok(ApiResponse<object>.SuccessResponse(result.Data));
        }

        [HttpDelete("{contentId}")]
        public async Task<IActionResult> Remove(int contentId)
        {
            var CurrentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var command = new RemoveBookmarkCommand { UserId = CurrentUserId!, ContentId = contentId };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetMine()
        {
            var CurrentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var query = new GetMyBookmarksQuery { UserId = CurrentUserId! };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
