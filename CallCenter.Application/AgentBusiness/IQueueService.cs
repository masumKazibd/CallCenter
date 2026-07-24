using CallCenter.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallCenter.Application.AgentBusiness
{
    public interface IQueueService
    {
        Task<IEnumerable<QueueDTO>> GetAllAsync();
        Task<QueueDTO?> GetByIdAsync(int id);
        Task<QueueDTO> CreateAsync(QueueDTO dto);
        Task<bool> UpdateAsync(int id, QueueDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
