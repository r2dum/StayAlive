using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnerState : BaseSpawnerState
{
    [Range(1f, 1.35f)] private float _timeSpawn;

    public RandomSpawnerState(IFactory factory, IStationStateSwitcher stateSwitcher, 
        List<Transform> spawnBombPositions, List<Transform> spawnWarnPositions, CurrentScore currentScore) 
        : base(factory, stateSwitcher, spawnBombPositions, spawnWarnPositions, currentScore)
    {
    }

    public override void Start()
    {
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
            if (_currentScore.Score % 15 == 0)
                _stateSwitcher.SwitchState<TriggerSpawnerState>();
            
            var position = RandomPosition();

            _factory.Spawn(_spawnBombPositions[position], GameContentType.Bomb);
            _factory.Spawn(_spawnWarnPositions[position], GameContentType.Warn);
        }
    }
    
    private int RandomPosition()
    {
        return Random.Range(0, _spawnBombPositions.Count);
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
