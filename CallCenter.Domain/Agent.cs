using CallCenter.Domain.Enums;
using System.Collections;
namespace CallCenter.Domain
{
    public class Agent
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Extension { get; set; } = string.Empty;
        public AgentStatus Status { get; set; }
        public int? QueueId { get; set; }
        public Queue? Queue { get; set; }
        public ICollection<Call> Calls { get; set; } = new List<Call>();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
