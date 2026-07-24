using CallCenter.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallCenter.Application.AgentBusiness
{
    public interface ICallService
    {
        Task<IEnumerable<CallDTO>> GetAllAsync();
        Task<CallDTO?> GetByIdAsync(int id);
        Task<CallDTO> CreateAsync(CallDTO dto);
        Task<bool> UpdateStatusAsync(int id, string status);
        Task<bool> DeleteAsync(int id);
    }
}
