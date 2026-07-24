using CallCenter.Application.AgentBusiness; 
using CallCenter.Application.Services;
using CallCenter.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace CallCenter.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CallsController : ControllerBase
    {
        private readonly ICallService _service;

        public CallsController(ICallService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var calls = await _service.GetAllAsync();
            return Ok(calls);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var call = await _service.GetByIdAsync(id);
            if (call == null)
            {
                return NotFound();
            }
            return Ok(call);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CallDTO dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}/status")]
        public async Task<IActionResult> UpdateStatus(int id, CallDTO dto)
        {
            var ok = await _service.UpdateStatusAsync(id, dto.Status);
            if (!ok)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id);
            if (!ok)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}