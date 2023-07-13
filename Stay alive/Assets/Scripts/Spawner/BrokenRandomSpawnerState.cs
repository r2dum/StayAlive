using UnityEngine;
using UnityEngine.UI;

public class BrokenRandomSpawnerState : RandomSpawnerState
{
    public BrokenRandomSpawnerState(IFactory factory, IStationStateSwitcher stateSwitcher, 
        Transform[] spawnBombPositions, Transform[] spawnWarnPositions, Text statusText) 
        : base(factory, stateSwitcher, spawnBombPositions, spawnWarnPositions, statusText)
    {
    }
    
    public override async void Start()
    {
        SetStateTime(10f, 15f);
        await ShowAndHideStatus("Broken Random Spawn");
    }
    
    public override void Stop()
    {
    }
    
    public override void Spawn()
    {
        if (StateTimeIsOver())
            _stateSwitcher.RandomSwitchState<InvisibleRandomSpawnerState, TriggerSpawnerState>();
        
        if (RandomTimeSpawn(0.35f, 1.25f))
        {
            _factory.Spawn(_spawnBombPositions[RandomPosition()], GameContentType.Bomb);
            _factory.Spawn(_spawnWarnPositions[RandomPosition()], GameContentType.Warn);
        }
    }
}
