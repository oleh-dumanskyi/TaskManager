﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Interfaces;

namespace TaskManager.WebAPI.Controllers
{
    [ApiController]
    [Route("/[controller]/[action]")]
    [Authorize]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _statusService;

        public StatusController(IStatusService service)
        {
            _statusService = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name, CancellationToken token)
        {
            try
            {
                var result = await _statusService.CreateStatus(name, token);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("/[controller]/[action]/{id}")]
        public async Task<IActionResult> Edit(Guid id, string name, CancellationToken token)
        {
            try
            {
                var result = await _statusService.EditStatus(id, name, token);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete]
        [Route("/[controller]/[action]/{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken token)
        {
            try
            {
                var result = await _statusService.DeleteStatus(id, token);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("/[controller]/[action]/{id}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken token)
        {
            try
            {
                var result = await _statusService.GetStatus(id, token);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetList(CancellationToken token)
        {
            var result = await _statusService.GetStatusList(token);
            return Ok(result);
        }
    }
}
