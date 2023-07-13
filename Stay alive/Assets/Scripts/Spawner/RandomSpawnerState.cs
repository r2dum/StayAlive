using UnityEngine;
using UnityEngine.UI;

public class RandomSpawnerState : BaseSpawnerState
{
    private float _spawnTime;
    private int _lastRandomPosition;
    
    public RandomSpawnerState(IFactory factory, IStationStateSwitcher stateSwitcher, 
        Transform[] spawnBombPositions, Transform[] spawnWarnPositions, Text statusText) 
        : base(factory, stateSwitcher, spawnBombPositions, spawnWarnPositions, statusText)
    {
    }

    public override async void Start()
    {
        SetStateTime(10f, 15f);
        await ShowAndHideStatus("Random Spawn");
    }

    public override void Stop()
    {
    }
    
    public override void Spawn()
    {
        if (StateTimeIsOver())
            _stateSwitcher.RandomSwitchState<BrokenRandomSpawnerState, TriggerSpawnerState>();
        
        RandomSpawn();
    }
    
    protected void RandomSpawn()
    {
        if (RandomTimeSpawn(0.35f, 1.25f))
        {
            var position = RandomPosition();

            _factory.Spawn(_spawnBombPositions[position], GameContentType.Bomb);
            _factory.Spawn(_spawnWarnPositions[position], GameContentType.Warn);
        }
    }
    
    protected bool RandomTimeSpawn(float minTime, float maxTime)
    {
        if (_spawnTime <= 0f)
        {
            _spawnTime = Random.Range(minTime, maxTime);
            return true;
        }

        _spawnTime -= Time.deltaTime;
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
}
