using CallCenter.Domain.Enums;
using System.Collections;
namespace CallCenter.Domain
{
    public class Agent
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Extension { get; set; }
        public AgentStatus Status { get; set; }
        public int? QueueId { get; set; }
        public Queue? Queue { get; set; }
        public ICollection<Call> Calls { get; set; } = new List<Call>();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
