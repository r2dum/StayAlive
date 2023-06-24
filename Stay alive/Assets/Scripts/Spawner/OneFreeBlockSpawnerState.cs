using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class OneFreeBlockSpawnerState : BaseSpawnerState
{
    private bool _isSpawned;
    
    public OneFreeBlockSpawnerState(IFactory factory, IStationStateSwitcher stateSwitcher, 
        Transform[] spawnBombPositions, Transform[] spawnWarnPositions, 
        CurrentScore currentScore, Text statusText) 
        : base(factory, stateSwitcher, spawnBombPositions, spawnWarnPositions, currentScore, statusText)
    {
    }
    
    public override async void Start()
    {
        _isSpawned = false;
        await ShowAndHideStatus("One block is free!!!");
    }

    public override void Stop()
    {
    }

    public override void Spawn()
    {
        if (_spawnBombPositions.All(s => s.gameObject.activeSelf == false) && _isSpawned == false)
        {
            var randomPositions = Enumerable.Range(0, _spawnBombPositions.Length)
                .OrderBy(_ => Guid.NewGuid()).Take(8);
            
            foreach (var position in randomPositions)
            {
                _factory.Spawn(_spawnBombPositions[position], GameContentType.Bomb);
                _factory.Spawn(_spawnWarnPositions[position], GameContentType.Warn);
            }
            
            _isSpawned = true;
        }
        
        if (_spawnBombPositions.All(s => s.gameObject.activeSelf == false))
            _stateSwitcher.RandomSwitchState<BrokenRandomSpawnerState, InvisibleRandomSpawnerState>();
    }
}
