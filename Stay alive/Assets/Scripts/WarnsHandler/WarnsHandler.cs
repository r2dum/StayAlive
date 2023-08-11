using System.Collections.Generic;
using UnityEngine;

public class WarnsHandler : IPauseHandler
{
    private readonly IFactory _factory;
    
    private readonly Dictionary<ISpawnable, Transform> _warns = 
        new Dictionary<ISpawnable, Transform>();
    public IReadOnlyDictionary<ISpawnable, Transform> Warns => _warns;

    public WarnsHandler(IFactory factory)
    {
        _factory = factory;
        _factory.WarnSpawned += AddToListWarn;
    }
    
    ~WarnsHandler()
    {
        _factory.WarnSpawned -= AddToListWarn;
    }

    private void AddToListWarn(ISpawnable warn, Transform position)
    {
        _warns.Add(warn, position);
        
        warn.Disabled += RemoveFromListWarn;
    }
    
    private void RemoveFromListWarn(ISpawnable warn)
    {
        warn.Disabled -= RemoveFromListWarn;

        _warns.TryGetValue(warn, out var position);
        position.gameObject.SetActive(false);
        _warns.Remove(warn);
    }
    
    public void SetPause(bool isPaused)
    {
        foreach (var warn in _warns)
        {
            warn.Key.SetPause(isPaused);
        }
    }
}
