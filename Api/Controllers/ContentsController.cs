using Application.Common;
using Application.Common.Models;
using Application.Feature.Content.Commands.CreateContent;
using Application.Feature.Content.Commands.ToggleContentStatus;
using Application.Feature.Content.Commands.UpdateContent;
using Application.Feature.Content.Queries.GetAllActive;
using Application.Feature.Content.Queries.GetContentById;
using Application.Feature.Content.Queries.GetContents;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ContentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(ContentFilterParams filters)
        {
            var result = await _mediator.Send(new GetContentsQuery(filters));
            return Ok(ApiResponse<object>.SuccessResponse(result.Data));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetContentByIdQuery { Id = id});
            return Ok(ApiResponse<object>.SuccessResponse(result.Data));
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create(CreateContentCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(ApiResponse<object>.SuccessResponse(result.Data));
        }

        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update(int id, UpdateContentCommand cmd)
        {
            cmd.Id = id;
            var result = await _mediator.Send(cmd);
            return Ok(ApiResponse<object>.SuccessResponse(result.Data));
        }

        [HttpPatch("{id}/toggle-status")]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var result = await _mediator.Send(new ToggleContentStatusCommand { ContentId = id});
            return Ok(ApiResponse<object>.SuccessResponse(result.Data));
        }

        [HttpGet("GetAllActive")]
        public async Task<IActionResult> GetAllActiveContent()
        {
            var result = await _mediator.Send(new GetAllActiveQuery());
            return Ok(ApiResponse<object>.SuccessResponse(result.Data));
        }
    }
}
