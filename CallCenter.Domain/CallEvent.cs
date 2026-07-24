using CallCenter.Domain.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CallCenter.Domain
{ 
    public class CallEvent
    {
        public int Id { get; set; } 
        public int CallId { get; set; }
        public Call Call { get; set; } = new Call();
        public string EventType { get; set; }  = string.Empty;
        public string? Details { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }

}
