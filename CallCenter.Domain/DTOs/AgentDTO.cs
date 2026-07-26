using System;
using System.Collections.Generic;
using System.Text;

namespace CallCenter.Domain.DTOs
{
    public class AgentDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Extension { get; set; }
        public string Status { get; set; }
        public int? QueueId { get; set; }
        public DateTime CreatedAt { get; set; }
        //custom
        public string? QueueName { get; set; }
    }
    public class UpdateStatusDTO
    {
        public string Status { get; set; }
    }
}
