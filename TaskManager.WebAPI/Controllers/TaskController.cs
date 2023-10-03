using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManager.Application.DTOs;
using TaskManager.Application.Interfaces;

namespace TaskManager.WebAPI.Controllers
{
    [ApiController]
    [Route("/[controller]/[action]")]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService service)
        {
            _taskService = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]TaskDTO request, CancellationToken token)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    request.AuthorId = Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                    var result = await _taskService.CreateTask(request, token);
                    return Ok(result);
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("/[controller]/[action]/{id}")]
        public async Task<IActionResult> Edit(Guid id, TaskDTO request, CancellationToken token)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _taskService.EditTask(id, request, token);
                    return Ok(result);
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetList(CancellationToken token)
        {
            var result = await _taskService.GetTaskList(token);
            return Ok(result);
        }

        [HttpGet]
        [Route("/[controller]/[action]/{id}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken token)
        {
            try
            {
                var result = await _taskService.GetTask(id, token);
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpDelete]
        [Route("/[controller]/[action]/{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken token)
        {
            try
            {
                var result = await _taskService.DeleteTask(id, token);
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
