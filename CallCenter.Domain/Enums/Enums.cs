namespace CallCenter.Domain.Enums
{
    public enum CallDirection
    { 
        Inbound, 
        Outbound
    }
    public enum CallStatus 
    { 
        Ringing, 
        InProgress,
        Completed, 
        Missed,
        Failed,
        Abandoned 
    }
    public enum AgentStatus 
    {
        Offline,
        Available,
        OnCall,
        Wrapup,
        NotReady 
    }
}
