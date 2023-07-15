using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BonusSpawner
{
    private readonly Transform[] _spawnBonusPositions;
    private readonly Transform[] _spawnBombPositions;
    
    private readonly IFactory _factory;
    private readonly BombsHandler _bombsHandler;
    
    public BonusSpawner(IFactory factory, BombsHandler bombsHandler, Transform[] spawnBonusPositions, 
        Transform[] spawnBombPositions)
    {
        _factory = factory;
        _bombsHandler = bombsHandler;
        _spawnBonusPositions = spawnBonusPositions;
        _spawnBombPositions = spawnBombPositions;
        
        _bombsHandler.BombDisabled += Spawn;
    }
    
    ~BonusSpawner()
    {
        _bombsHandler.BombDisabled -= Spawn;
    }
    
    private void Spawn(ISpawnable bomb, Transform bombPosition)
    {
        var chance = Random.Range(0, 101);
        var position = Array.IndexOf(_spawnBombPositions, bombPosition);
        
        switch (chance)
        {
            case <= 3:
            {
                _factory.Spawn(_spawnBonusPositions[position], GameContentType.Armour);
                break;
            }
            case <= 40:
            {
                _factory.Spawn(_spawnBonusPositions[position], GameContentType.Coin);
                break;
            }
        }
    }
}
