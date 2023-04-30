using System.Collections.Generic;

public class PauseHandler : IPauseHandler
{
    private readonly List<IPauseHandler> _handlers = 
        new List<IPauseHandler>();
    
    public void AddToPauseList(IPauseHandler handler)
    {
        _handlers.Add(handler);
    }
    
    public void RemoveFromPauseList(IPauseHandler handler)
    {
        _handlers.Remove(handler);
    }
    
    public void SetPause(bool isPaused)
    {
        foreach (var handler in _handlers)
            handler.SetPause(isPaused);
    }
}
