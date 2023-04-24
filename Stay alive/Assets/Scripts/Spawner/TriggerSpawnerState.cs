using System.Collections.Generic;
using UnityEngine;

public class TriggerSpawnerState : BaseSpawnerState
{
    private TriggerBlock[] _triggerBlocks;

    public TriggerSpawnerState(IFactory factory, IStationStateSwitcher stateSwitcher, 
        List<Transform> spawnBombPositions, List<Transform> spawnWarnPositions, 
        CurrentScore currentScore, TriggerBlock[] triggerBlocks) 
        : base(factory, stateSwitcher, spawnBombPositions, spawnWarnPositions, currentScore)
    {
        _triggerBlocks = triggerBlocks;
    }

    public override void Start()
    {
        Debug.Log("Trigger Start");
        
        for (int i = 0; i < _triggerBlocks.Length; i++)
        {
            _triggerBlocks[i].Triggered += OnTriggered;
        }
    }

    public override void Stop()
    {
        Debug.Log("Trigger Stop");
        
        for (int i = 0; i < _triggerBlocks.Length; i++)
        {
            _triggerBlocks[i].Triggered -= OnTriggered;
        }
    }

    public override void Spawn()
    {
    }
    
    private void OnTriggered(int position)
    {
        _factory.Spawn(_spawnBombPositions[position], GameContentType.Bomb);
        _factory.Spawn(_spawnWarnPositions[position], GameContentType.Warn);
    }
}
