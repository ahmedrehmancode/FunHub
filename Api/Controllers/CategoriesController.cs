using Application.Common;
using Application.Feature.Category.Commands.ToggleCategoryStatus;
using Application.Feature.Category.Commands.UpdateCategory;
using Application.Feature.Category.Queries.GetActiveCategories;
using Application.Feature.Category.Queries.GetAllCategory;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActiveCategory()
        {
            var result = await _mediator.Send(new GetActiveCategoryQuery());
            return Ok(ApiResponse<object>.SuccessResponse(result.Data));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllCategory()
        {
            var result = await _mediator.Send(new GetAllCategoryQuery());
            return Ok(ApiResponse<object>.SuccessResponse(result.Data));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryCommand cmd)
        {
            cmd.cId = id;
            var result = await _mediator.Send(cmd);
            return Ok(ApiResponse<object>.SuccessResponse(result.Data));
        }


        [HttpPatch("{id}/toggle-status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var result = await _mediator.Send(new ToggleCategoryStatusCommand
            {
                cId = id
            });
            return Ok(ApiResponse<object>.SuccessResponse(result.Data));
        }
    }
}
