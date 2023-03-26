using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnerState : BaseSpawnerState
{
    [Range(1f, 1.35f)] private float _startSpawn;
    private float _timeSpawn;
    private int _randomPosition;
    
    private readonly List<Transform> _spawnWarnPositions;
    
    public RandomSpawnerState(IFactory factory, IStationStateSwitcher stateSwitcher, 
        List<Transform> spawnPositions, List<Transform> spawnWarnPositions) 
        : base(factory, stateSwitcher, spawnPositions)
    {
        _spawnWarnPositions = spawnWarnPositions;
    }

    public override void Start()
    {
        _timeSpawn = _startSpawn;
        Debug.Log("Random Start");
    }

    public override void Stop()
    {
        Debug.Log("Random Stop");
    }
    
    public override void Spawn()
    {
        if (RandomTime(0.35f, 1.25f))
        {
            var position = RandomPosition();
            
            _factory.Spawn(_spawnPositions[position], GameContentType.Bomb);
            _factory.Spawn(_spawnWarnPositions[position], GameContentType.Warn);
        }
    }
    
    private int RandomPosition()
    {
        return Random.Range(0, _spawnPositions.Count);
    }
    
    private bool RandomTime(float minTime, float maxTime)
    {
        if (_timeSpawn <= 0f)
        {
            _timeSpawn = Random.Range(minTime, maxTime);
            return true;
        }

        _timeSpawn -= Time.deltaTime;
        return false;
    }
}
