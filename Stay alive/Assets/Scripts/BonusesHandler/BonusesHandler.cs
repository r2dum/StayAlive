using System.Collections.Generic;
using UnityEngine;

public class BonusesHandler : IPauseHandler
{
    private readonly GameContentFactory _factory;
    
    private readonly Dictionary<ISpawnable, Transform> _bonuses = 
        new Dictionary<ISpawnable, Transform>();
    public IReadOnlyDictionary<ISpawnable, Transform> Bonuses => _bonuses;
    
    public BonusesHandler(GameContentFactory factory)
    {
        _factory = factory;
        _factory.BonusSpawned += AddToListBonus;
    }
    
    ~BonusesHandler()
    {
        _factory.BonusSpawned -= AddToListBonus;
    }
    
    private void AddToListBonus(ISpawnable bonus, Transform position)
    {
        _bonuses.Add(bonus, position);
        
        bonus.Disabled += RemoveFromListBonus;
    }
    
    private void RemoveFromListBonus(ISpawnable bonus)
    {
        bonus.Disabled -= RemoveFromListBonus;
        
        _bonuses.TryGetValue(bonus, out var position);
        position.gameObject.SetActive(false);
        _bonuses.Remove(bonus);
    }
    
    public void SetPause(bool isPaused)
    {
        foreach (var bonus in _bonuses)
        {
            bonus.Key.SetPause(isPaused);
        }
    }
}
