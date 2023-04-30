using System;
using System.Collections.Generic;

public class BombsHandler : IPauseHandler
{
    private readonly GameContentFactory _factory;
    
    private readonly List<Bomb> _bombs = new List<Bomb>();
    public IReadOnlyCollection<Bomb> Bombs => _bombs;
    
    public event Action<Bomb> BombDropped;
    
    public BombsHandler(GameContentFactory factory)
    {
        _factory = factory;
        _factory.BombSpawned += AddToListBomb;
    }
    
    ~BombsHandler()
    {
        _factory.BombSpawned -= AddToListBomb;
    }

    private void AddToListBomb(Bomb bomb)
    {
        _bombs.Add(bomb);

        bomb.Dropped += RemoveFromListBomb;
        bomb.Dropped += OnBombDropped;
    }
    
    private void RemoveFromListBomb(Bomb bomb)
    {
        bomb.Dropped -= RemoveFromListBomb;
        bomb.Dropped -= OnBombDropped;
        
        _bombs.Remove(bomb);
    }
    
    private void OnBombDropped(Bomb bomb)
    {
        BombDropped?.Invoke(bomb);
    }
    
    public void SetPause(bool isPaused)
    {
        foreach (var bomb in _bombs)
        {
            bomb.SetPause(isPaused);
        }
    }
}
