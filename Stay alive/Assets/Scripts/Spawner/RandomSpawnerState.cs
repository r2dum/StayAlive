using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RandomSpawnerState : BaseSpawnerState
{
    private readonly Transform[] _spawnBonusPositions;
    
    private float _timeSpawn;
    private int _lastRandomPosition;
    
    protected readonly BombsHandler _bombsHandler;
    
    public RandomSpawnerState(IFactory factory, IStationStateSwitcher stateSwitcher, 
        Transform[] spawnBombPositions, Transform[] spawnWarnPositions, Transform[] spawnBonusPositions,
        CurrentScore currentScore, Text statusText, BombsHandler bombsHandler) 
        : base(factory, stateSwitcher, spawnBombPositions, spawnWarnPositions, currentScore, statusText)
    {
        _spawnBonusPositions = spawnBonusPositions;
        _bombsHandler = bombsHandler;
    }

    public override async void Start()
    {
        _bombsHandler.BombDisabled += SpawnBonus;
        await ShowAndHideStatus("Random Spawn");
    }

    public override void Stop()
    {
        _bombsHandler.BombDisabled -= SpawnBonus;
    }
    
    public override void Spawn()
    {
        if (_currentScore.Amount % 14 == 0 && _currentScore.Amount != 0)
            _stateSwitcher.RandomSwitchState<BrokenRandomSpawnerState, TriggerSpawnerState>();
        
        RandomSpawn();
    }
    
    protected void RandomSpawn()
    {
        if (RandomTime(0.35f, 1.25f))
        {
            var position = RandomPosition();

            _factory.Spawn(_spawnBombPositions[position], GameContentType.Bomb);
            _factory.Spawn(_spawnWarnPositions[position], GameContentType.Warn);
        }
    }
    
    protected void SpawnBonus(ISpawnable bomb, Transform bombPosition)
    {
        if (Chance(50))
        {
            var position = Array.IndexOf(_spawnBombPositions, bombPosition);
            _factory.Spawn(_spawnBonusPositions[position], GameContentType.Coin);
        }
    }
    
    protected bool RandomTime(float minTime, float maxTime)
    {
        if (_timeSpawn <= 0f)
        {
            _timeSpawn = Random.Range(minTime, maxTime);
            return true;
        }

        _timeSpawn -= Time.deltaTime;
        return false;
    }
    
    protected int RandomPosition()
    {
        var position = Random.Range(0, _spawnBombPositions.Length);
        
        while (_lastRandomPosition == position)
            position = Random.Range(0, _spawnBombPositions.Length);

        _lastRandomPosition = position;
        return position;
    }
    
    private bool Chance(int chance)
    {
        return Random.Range(0, 101) < chance;
    }
}
