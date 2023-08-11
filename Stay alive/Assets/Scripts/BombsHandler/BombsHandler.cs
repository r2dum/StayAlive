using System;
using System.Collections.Generic;
using UnityEngine;

public class BombsHandler : IPauseHandler
{
    private readonly IFactory _factory;
    
    private readonly Dictionary<ISpawnable, Transform> _bombs = 
        new Dictionary<ISpawnable, Transform>();
    public IReadOnlyDictionary<ISpawnable, Transform> Bombs => _bombs;
    
    public event Action<ISpawnable, Transform> BombDisabled;
    
    public BombsHandler(IFactory factory)
    {
        _factory = factory;
        _factory.BombSpawned += AddToListBomb;
    }
    
    ~BombsHandler()
    {
        _factory.BombSpawned -= AddToListBomb;
    }

    private void AddToListBomb(ISpawnable bomb, Transform position)
    {
        _bombs.Add(bomb, position);
        bomb.Disabled += RemoveFromListBomb;
    }
    
    private void RemoveFromListBomb(ISpawnable bomb)
    {
        bomb.Disabled -= RemoveFromListBomb;
        
        _bombs.TryGetValue(bomb, out var position);
        BombDisabled?.Invoke(bomb, position);
        position.gameObject.SetActive(false);
        _bombs.Remove(bomb);
    }
    
    public void SetPause(bool isPaused)
    {
        foreach (var bomb in _bombs)
        {
            bomb.Key.SetPause(isPaused);
        }
    }
}
