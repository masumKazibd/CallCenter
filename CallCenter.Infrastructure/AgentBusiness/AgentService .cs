using CallCenter.Infrastructure.Data;
using CallCenter.Domain.DTOs;
using CallCenter.Application.Services;
using CallCenter.Domain; 
using CallCenter.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace CallCenter.Infrastructure.Services
{
    public class AgentService : IAgentService
    {
        private readonly AppDbContext _dbContext;

        public AgentService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<AgentDTO>> GetAllAsync()
        {
            var agents = await _dbContext.Agents.ToListAsync(); 
            var queues = await _dbContext.Queues.ToListAsync();

            var result = new List<AgentDTO>();
            foreach (var a in agents)
            {
                var queueName = queues.FirstOrDefault(q => q.Id == a.QueueId)?.Name;
                result.Add(new AgentDTO
                {
                    Id = a.Id,
                    FullName = a.FullName,
                    Email = a.Email,
                    Extension = a.Extension,
                    Status = a.Status.ToString(),
                    QueueId = a.QueueId,
                    QueueName = queueName,
                    CreatedAt = a.CreatedAt
                });
            }
            return result;
        }

        public async Task<AgentDTO?> GetByIdAsync(int id)
        {
            var a = await _dbContext.Agents.FindAsync(id);
            if (a == null)
            {
                return null;
            }

            return new AgentDTO
            {
                Id = a.Id,
                FullName = a.FullName,
                Email = a.Email,
                Extension = a.Extension,
                Status = a.Status.ToString(),
                QueueId = a.QueueId,
                CreatedAt = a.CreatedAt
            };
        }

        public async Task<AgentDTO> CreateAsync(AgentDTO dto)
        {
            var agent = new Agent
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Extension = dto.Extension,
                QueueId = dto.QueueId,
                Status = AgentStatus.Offline
            };

            _dbContext.Agents.Add(agent);
            await _dbContext.SaveChangesAsync();

            dto.Id = agent.Id;
            dto.Status = agent.Status.ToString();
            dto.CreatedAt = agent.CreatedAt;
            return dto;
        }

        public async Task<bool> UpdateStatusAsync(int id, string status)
        {
            var agent = await _dbContext    .Agents.FindAsync(id);
            if (agent == null)
            {
                return false;
            }

            AgentStatus parsed;
            bool ok = Enum.TryParse<AgentStatus>(status, true, out parsed);
            if (!ok)
            {
                return false;
            }

            agent.Status = parsed;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var agent = await _dbContext.Agents.FindAsync(id);
            if (agent == null)
            {
                return false;
            }

            _dbContext.Agents.Remove(agent);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<AgentDTO?> GetAvailableAgentByQueueAsync(int queueId)
        {
            var agent = await _dbContext.Agents.FirstOrDefaultAsync(a => a.QueueId == queueId && a.Status == AgentStatus.Available);
            if (agent == null)
            {
                return null;
            }

            return new AgentDTO
            {
                Id = agent.Id,
                FullName = agent.FullName,
                Email = agent.Email,
                Extension = agent.Extension,
                Status = agent.Status.ToString(),
                QueueId = agent.QueueId,
                CreatedAt = agent.CreatedAt
            };
        }

    }
}