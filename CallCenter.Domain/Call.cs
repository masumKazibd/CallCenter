using CallCenter.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CallCenter.Domain
{ 
    public class Call
    {
        public int Id { get; set; }
        public Enums.CallDirection Direction { get; set; }
        public string FromNumber { get; set; } = string.Empty;
        public string ToNumber { get; set; } = string.Empty;
        public Enums.CallStatus Status { get; set; }

        public int? AgentId { get; set; }
        public Agent? Agent { get; set; }
        public int? QueueId { get; set; }
        public Queue? Queue { get; set; }

        public DateTime StartedAt { get; set; } = DateTime.UtcNow;
        public DateTime? AnsweredAt { get; set; }
        public DateTime? EndedAt { get; set; }
        public int DurationSeconds { get; set; }

        public string? RecordingUrl { get; set; }
        public string? CrmCustomerId { get; set; }

        public ICollection<CallEvent> Events { get; set; } = new List<CallEvent>();
    }
}
