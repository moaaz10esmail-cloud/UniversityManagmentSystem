using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystem.Core.Common;

namespace UniversityManagementSystem.API.Controllers.Base
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        private IMediator? _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;

        protected IActionResult HandleResult<T>(Result<T> result)
        {
            if (result == null) 
                return NotFound();
            
            if (result.Succeeded && result.Data != null)
                return Ok(result);
                
            if (result.Succeeded && result.Data == null)
                return NotFound();
                
            return BadRequest(result);
        }

        protected IActionResult HandleResult(Result result)
        {
            if (result.Succeeded)
                return Ok(result);
                
            return BadRequest(result);
        }

        protected IActionResult HandlePagedResult<T>(Result<PagedList<T>> result)
        {
            if (result == null) 
                return NotFound();
            
            if (result.Succeeded && result.Data != null)
            {
                Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(new 
                {
                    result.Data.CurrentPage,
                    result.Data.PageSize,
                    result.Data.TotalCount,
                    result.Data.TotalPages
                }));
                return Ok(result);
            }
                
            if (result.Succeeded && result.Data == null)
                return NotFound();
                
            return BadRequest(result);
        }
    }
}
