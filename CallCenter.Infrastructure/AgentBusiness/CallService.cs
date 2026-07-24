using CallCenter.Application.AgentBusiness; 
using CallCenter.Application.Services;
using CallCenter.Domain;
using CallCenter.Domain.DTOs;
using CallCenter.Domain.Enums;
using CallCenter.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CallCenter.Infrastructure.Services
{
    public class CallService : ICallService
    {
        private readonly AppDbContext _db;

        public CallService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<CallDTO>> GetAllAsync()
        {
            var calls = await _db.Calls.ToListAsync();

            var result = new List<CallDTO>();
            foreach (var c in calls)
            {
                result.Add(MapToDto(c));
            }
            return result;
        }

        public async Task<CallDTO?> GetByIdAsync(int id)
        {
            var c = await _db.Calls.FindAsync(id);
            if (c == null)
            {
                return null;
            }
            return MapToDto(c);
        }

        public async Task<CallDTO> CreateAsync(CallDTO dto)
        {
            CallDirection direction;
            Enum.TryParse<CallDirection>(dto.Direction, true, out direction);

            var call = new Call
            {
                Direction = direction,
                FromNumber = dto.FromNumber,
                ToNumber = dto.ToNumber,
                Status = CallStatus.Ringing,
                AgentId = dto.AgentId,
                QueueId = dto.QueueId,
                StartedAt = DateTime.UtcNow
            };

            _db.Calls.Add(call);
            await _db.SaveChangesAsync();

            return MapToDto(call);
        }

        public async Task<bool> UpdateStatusAsync(int id, string status)
        {
            var call = await _db.Calls.FindAsync(id);
            if (call == null)
            {
                return false;
            }

            CallStatus parsed;
            bool ok = Enum.TryParse<CallStatus>(status, true, out parsed);
            if (!ok)
            {
                return false;
            }

            call.Status = parsed;

            // when a call completes, stamp the end time and compute duration
            if (parsed == CallStatus.Completed)
            {
                call.EndedAt = DateTime.UtcNow;
                call.DurationSeconds = (int)(call.EndedAt.Value - call.StartedAt).TotalSeconds;
            }

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var call = await _db.Calls.FindAsync(id);
            if (call == null)
            {
                return false;
            }

            _db.Calls.Remove(call);
            await _db.SaveChangesAsync();
            return true;
        }

        private CallDTO MapToDto(Call c)
        {
            return new CallDTO
            {
                Id = c.Id,
                Direction = c.Direction.ToString(),
                FromNumber = c.FromNumber,
                ToNumber = c.ToNumber,
                Status = c.Status.ToString(),
                AgentId = c.AgentId,
                QueueId = c.QueueId,
                StartedAt = c.StartedAt,
                AnsweredAt = c.AnsweredAt,
                EndedAt = c.EndedAt,
                DurationSeconds = c.DurationSeconds,
                RecordingUrl = c.RecordingUrl,
                CrmCustomerId = c.CrmCustomerId
            };
        }
    }
}