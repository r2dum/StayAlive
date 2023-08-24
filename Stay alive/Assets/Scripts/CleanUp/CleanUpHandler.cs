using System.Collections.Generic;

public class CleanUpHandler : ICleanUp
{
    private readonly List<ICleanUp> _handlers = 
        new List<ICleanUp>();
    
    public void AddToCleanList(ICleanUp handler)
    {
        _handlers.Add(handler);
    }
    
    public void CleanUp()
    {
        foreach (var handler in _handlers)
            handler.CleanUp();
    }
}
