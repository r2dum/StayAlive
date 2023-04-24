using System.Collections.Generic;
using UnityEngine;

public class InvisibleRandomSpawnerState : BaseSpawnerState
{
    private float _timeSpawn;
    
    public InvisibleRandomSpawnerState(IFactory factory, IStationStateSwitcher stateSwitcher, 
        List<Transform> spawnBombPositions, List<Transform> spawnWarnPositions, CurrentScore currentScore) 
        : base(factory, stateSwitcher, spawnBombPositions, spawnWarnPositions, currentScore)
    {
    }

    public override void Start()
    {
        Debug.Log("Invisible Random Start");
    }

    public override void Stop()
    {
        Debug.Log("Invisible Random Stop");
    }

    public override void Spawn()
    {
        if (RandomTime(0.35f, 1.25f))
        {
            if (_currentScore.Score % 83 == 0)
                _stateSwitcher.SwitchState<RandomSpawnerState>();
            
            _factory.Spawn(_spawnBombPositions.RandomPosition(), GameContentType.Bomb);
        }
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
