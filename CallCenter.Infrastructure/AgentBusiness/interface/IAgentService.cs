using CallCenter.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallCenter.Application.Services
{
    public interface IAgentService
    {
        Task<IEnumerable<AgentDTO>> GetAllAsync();
        Task<AgentDTO?> GetByIdAsync(int id);
        Task<AgentDTO> CreateAsync(AgentDTO dto);
        Task<bool> UpdateStatusAsync(int id, string status);
        Task<bool> DeleteAsync(int id);
    }
}