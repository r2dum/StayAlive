using System;
using UnityEngine;
using UnityEngine.UI;

public class TriggerSpawnerState : RandomSpawnerState, IStoppable
{
    private readonly TriggerBlock[] _triggerBlocks;

    public TriggerSpawnerState(IFactory factory, IStationStateSwitcher stateSwitcher, 
        Transform[] spawnBombPositions, Transform[] spawnWarnPositions, Text statusText, TriggerBlock[] triggerBlock) 
        : base(factory, stateSwitcher, spawnBombPositions, spawnWarnPositions, statusText)
    {
        _triggerBlocks = triggerBlock;
    }
    
    public override async void Start()
    {
        SetStateTime(10f, 15f);
        
        foreach (var block in _triggerBlocks)
        {
            block.gameObject.SetActive(true);
            block.Triggered += OnTriggered;
        }
        
        await ShowAndHideStatus("Trigger Spawn");
    }
    
    public void Stop()
    {
        foreach (var block in _triggerBlocks)
        {
            block.gameObject.SetActive(false);
            block.Triggered -= OnTriggered;
        }
    }
    
    public override void Spawn()
    {
        if (StateTimeIsOver())
            _stateSwitcher.RandomSwitchState<OneFreeBlockSpawnerState, InvisibleRandomSpawnerState>();
        
        RandomSpawn();
    }
    
    private void OnTriggered(TriggerBlock triggerBlock)
    {
        var position = Array.IndexOf(_triggerBlocks, triggerBlock);
        
        _factory.Spawn(_spawnBombPositions[position], GameContentType.Bomb);
        _factory.Spawn(_spawnWarnPositions[position], GameContentType.Warn);
    }
}
