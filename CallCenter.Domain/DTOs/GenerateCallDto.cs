using System;
using System.Collections.Generic;
using System.Text;

namespace CallCenter.Domain.DTOs
{
    public class GenerateCallDto
    {
        public string CustomerPhoneNumber { get; set; } = string.Empty;
        public int QueueId { get; set; }
    }
}
