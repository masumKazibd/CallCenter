using CallCenter.Application.AgentBusiness;
using CallCenter.Application.Services;
using CallCenter.Domain.DTOs; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CallCenter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimulationController : ControllerBase
    {
        private readonly IHubContext<CallHub.CallHub> _hubContext;
        private readonly IAgentService _agentService;
        private readonly ICallService _callService;

        public SimulationController(IHubContext<CallHub.CallHub> hubContext, IAgentService agentService, ICallService callService)
        {
            _hubContext = hubContext;
            _agentService = agentService;
            _callService = callService;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateCall([FromBody] GenerateCallDto request)
        {
            try
            { 
                var availableAgent = await _agentService.GetAvailableAgentByQueueAsync(request.QueueId);

                if (availableAgent == null)
                {
                    return BadRequest(new { Message = "No available agents in this queue right now." });
                }

                var newCallDto = new CallDTO
                {
                    FromNumber = request.CustomerPhoneNumber,
                    ToNumber = "System",  
                    Direction = "Inbound",
                    QueueId = request.QueueId,
                    AgentId = availableAgent.Id,
                    Status = "Ringing"
                };

                var savedCall = await _callService.CreateAsync(newCallDto);

                await _hubContext.Clients.All.SendAsync("ReceiveCall", savedCall);

                return Ok(new { Message = "Call generated and routed successfully!", CallDetails = savedCall });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred", Error = ex.Message });
            }
        }
    }
}