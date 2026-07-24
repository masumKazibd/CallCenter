using System;
using System.Collections.Generic;
using System.Text;

namespace CallCenter.Domain.DTOs
{
    public class CallDTO
    {
        public int Id { get; set; }
        public string Direction { get; set; }   // "Inbound" or "Outbound"
        public string FromNumber { get; set; }
        public string ToNumber { get; set; }
        public string Status { get; set; }       // set by server
        public int? AgentId { get; set; }
        public int? QueueId { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? AnsweredAt { get; set; }
        public DateTime? EndedAt { get; set; }
        public int DurationSeconds { get; set; }
        public string? RecordingUrl { get; set; }
        public string? CrmCustomerId { get; set; }
    }
}
