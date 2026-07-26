using CallCenter.Application.AgentBusiness;
using CallCenter.Application.Services;
using CallCenter.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CallCenter.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgentsController : ControllerBase
    {
        private readonly IAgentService _agentService;

        public AgentsController(IAgentService service)
        {
            _agentService = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var agents = await _agentService.GetAllAsync();
            return Ok(agents);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var agent = await _agentService.GetByIdAsync(id);
            if (agent == null)
            {
                return NotFound();
            }
            return Ok(agent);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AgentDTO agent)
        {
            var created = await _agentService.CreateAsync(agent);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}/status")]
        public async Task<IActionResult> UpdateStatus(int id, UpdateStatusDTO dto)
        {
            var ok = await _agentService.UpdateStatusAsync(id, dto.Status);
            if (!ok)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _agentService.DeleteAsync(id);
            if (!ok)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}