using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSpawnerState
{
    protected readonly IFactory _factory;
    protected readonly IStationStateSwitcher _stateSwitcher;
    protected readonly List<Transform> _spawnPositions;

    protected BaseSpawnerState(IFactory factory, IStationStateSwitcher stateSwitcher, List<Transform> spawnPositions)
    {
        _factory = factory;
        _stateSwitcher = stateSwitcher;
        _spawnPositions = spawnPositions;
    }

    public abstract void Start();
    public abstract void Stop();

    public abstract void Spawn();
}