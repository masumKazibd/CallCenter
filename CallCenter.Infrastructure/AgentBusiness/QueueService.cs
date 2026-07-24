using CallCenter.Application.AgentBusiness; 
using CallCenter.Application.Services;
using CallCenter.Domain;
using CallCenter.Domain.DTOs;
using CallCenter.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CallCenter.Infrastructure.Services
{
    public class QueueService : IQueueService
    {
        private readonly AppDbContext _db;

        public QueueService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<QueueDTO>> GetAllAsync()
        {
            var queues = await _db.Queues.ToListAsync();

            var result = new List<QueueDTO>();
            foreach (var q in queues)
            {
                result.Add(new QueueDTO
                {
                    Id = q.Id,
                    Name = q.Name,
                    Description = q.Description,
                    CreatedAt = q.CreatedAt
                });
            }
            return result;
        }

        public async Task<QueueDTO?> GetByIdAsync(int id)
        {
            var q = await _db.Queues.FindAsync(id);
            if (q == null)
            {
                return null;
            }

            return new QueueDTO
            {
                Id = q.Id,
                Name = q.Name,
                Description = q.Description,
                CreatedAt = q.CreatedAt
            };
        }

        public async Task<QueueDTO> CreateAsync(QueueDTO dto)
        {
            var queue = new Queue
            {
                Name = dto.Name,
                Description = dto.Description
            };

            _db.Queues.Add(queue);
            await _db.SaveChangesAsync();

            dto.Id = queue.Id;
            dto.CreatedAt = queue.CreatedAt;
            return dto;
        }

        public async Task<bool> UpdateAsync(int id, QueueDTO dto)
        {
            var queue = await _db.Queues.FindAsync(id);
            if (queue == null)
            {
                return false;
            }

            queue.Name = dto.Name;
            queue.Description = dto.Description;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var queue = await _db.Queues.FindAsync(id);
            if (queue == null)
            {
                return false;
            }

            _db.Queues.Remove(queue);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}