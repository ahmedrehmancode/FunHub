using Application.Common;
using Application.Feature.Merchandise.Commands.CreateMerchandise;
using Application.Feature.Merchandise.Commands.ToggleMerchandiseStatus;
using Application.Feature.Merchandise.Queries.GetMerchandiseByCategory;
using Application.Feature.Merchandise.Queries.GetUpcomingMerchandise;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MerchandiseController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MerchandiseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetByCategory(int categoryId)
        {
            var result = await _mediator.Send(new GetMerchandiseByCategoryQuery { CategoryId = categoryId });
            return Ok(ApiResponse<object>.SuccessResponse(result.Data));
        }

        [HttpGet("upcoming")]
        public async Task<IActionResult> GetUpcoming()
        {
            var result = await _mediator.Send(new GetUpcomingMerchandiseQuery());
            return Ok(ApiResponse<object>.SuccessResponse(result.Data));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create(CreateMerchandiseCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(ApiResponse<object>.SuccessResponse(result.Data));
        }

        [HttpPatch("{id}/toggle-status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var result = await _mediator.Send(new ToggleMerchandiseStatusCommand { Id = id });
            return Ok(ApiResponse<object>.SuccessResponse(result.Data));
        }
    }
}
