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
        private readonly AppDbContext _db;

        public AgentService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<AgentDTO>> GetAllAsync()
        {
            var agents = await _db.Agents.ToListAsync();

            var result = new List<AgentDTO>();
            foreach (var a in agents)
            {
                result.Add(new AgentDTO
                {
                    Id = a.Id,
                    FullName = a.FullName,
                    Email = a.Email,
                    Extension = a.Extension,
                    Status = a.Status.ToString(),
                    QueueId = a.QueueId,
                    CreatedAt = a.CreatedAt
                });
            }
            return result;
        }

        public async Task<AgentDTO?> GetByIdAsync(int id)
        {
            var a = await _db.Agents.FindAsync(id);
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

            _db.Agents.Add(agent);
            await _db.SaveChangesAsync();

            dto.Id = agent.Id;
            dto.Status = agent.Status.ToString();
            dto.CreatedAt = agent.CreatedAt;
            return dto;
        }

        public async Task<bool> UpdateStatusAsync(int id, string status)
        {
            var agent = await _db.Agents.FindAsync(id);
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
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var agent = await _db.Agents.FindAsync(id);
            if (agent == null)
            {
                return false;
            }

            _db.Agents.Remove(agent);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}