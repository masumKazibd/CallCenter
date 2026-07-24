using CallCenter.Application.AgentBusiness; 
using CallCenter.Application.Services;
using CallCenter.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CallCenter.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QueuesController : ControllerBase
    {
        private readonly IQueueService _service;

        public QueuesController(IQueueService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var queues = await _service.GetAllAsync();
            return Ok(queues);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var queue = await _service.GetByIdAsync(id);
            if (queue == null)
            {
                return NotFound();
            }
            return Ok(queue);
        }

        [HttpPost]
        public async Task<IActionResult> Create(QueueDTO dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, QueueDTO dto)
        {
            var ok = await _service.UpdateAsync(id, dto);
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