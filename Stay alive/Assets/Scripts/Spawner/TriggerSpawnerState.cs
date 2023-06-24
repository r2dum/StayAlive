using System;
using UnityEngine;
using UnityEngine.UI;

public class TriggerSpawnerState : RandomSpawnerState
{
    private readonly TriggerBlock[] _triggerBlocks;

    public TriggerSpawnerState(IFactory factory, IStationStateSwitcher stateSwitcher, 
        Transform[] spawnBombPositions, Transform[] spawnWarnPositions, Transform[] spawnBonusPositions, 
        CurrentScore currentScore, Text statusText, BombsHandler bombsHandler, TriggerBlock[] triggerBlock) 
        : base(factory, stateSwitcher, spawnBombPositions, spawnWarnPositions,
            spawnBonusPositions, currentScore, statusText, bombsHandler)
    {
        _triggerBlocks = triggerBlock;
    }
    
    public override async void Start()
    {
        _bombsHandler.BombDisabled += SpawnBonus;
        
        foreach (var block in _triggerBlocks)
        {
            block.gameObject.SetActive(true);
            block.Triggered += OnTriggered;
        }
        
        await ShowAndHideStatus("Trigger Spawn");
    }
    
    public override void Stop()
    {
        _bombsHandler.BombDisabled -= SpawnBonus;
        
        foreach (var block in _triggerBlocks)
        {
            block.gameObject.SetActive(false);
            block.Triggered -= OnTriggered;
        }
    }
    
    public override void Spawn()
    {
        if (_currentScore.Amount % 42 == 0)
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
