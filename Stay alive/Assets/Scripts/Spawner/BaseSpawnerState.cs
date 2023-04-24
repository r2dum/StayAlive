using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSpawnerState
{
    protected readonly IFactory _factory;
    protected readonly IStationStateSwitcher _stateSwitcher;
    protected readonly List<Transform> _spawnBombPositions;
    protected readonly List<Transform> _spawnWarnPositions;
    protected readonly CurrentScore _currentScore;

    protected BaseSpawnerState(IFactory factory, IStationStateSwitcher stateSwitcher, 
        List<Transform> spawnBombPositions, List<Transform> spawnWarnPositions, CurrentScore currentScore)
    {
        _factory = factory;
        _stateSwitcher = stateSwitcher;
        _spawnBombPositions = spawnBombPositions;
        _spawnWarnPositions = spawnWarnPositions;
        _currentScore = currentScore;
    }

    public abstract void Start();
    public abstract void Stop();

    public abstract void Spawn();
}