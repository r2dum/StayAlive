using System;
using System.Collections.Generic;

public class WarnsHandler : IPauseHandler
{
    private readonly GameContentFactory _factory;
    
    private readonly List<Warn> _warns = new List<Warn>();
    public IReadOnlyCollection<Warn> Warns => _warns;
    
    public event Action<Warn> WarnDisabled;
    
    public WarnsHandler(GameContentFactory factory)
    {
        _factory = factory;
        _factory.WarnSpawned += AddToListWarn;
    }
    
    ~WarnsHandler()
    {
        _factory.WarnSpawned -= AddToListWarn;
    }

    private void AddToListWarn(Warn warn)
    {
        _warns.Add(warn);

        warn.Disabled += RemoveFromListWarn;
        warn.Disabled += OnWarnDisabled;
    }
    
    private void RemoveFromListWarn(Warn warn)
    {
        warn.Disabled -= RemoveFromListWarn;
        warn.Disabled -= OnWarnDisabled;
        
        _warns.Remove(warn);
    }
    
    private void OnWarnDisabled(Warn warn)
    {
        WarnDisabled?.Invoke(warn);
    }
    
    public void SetPause(bool isPaused)
    {
        foreach (var warn in _warns)
        {
            warn.SetPause(isPaused);
        }
    }
}
