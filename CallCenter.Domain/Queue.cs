using CallCenter.Domain.;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CallCenter.Domain
{ 
    public class Queue
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ICollection<Agent> Agents { get; set; } = new List<Agent>();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
