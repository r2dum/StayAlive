using UnityEngine;
using UnityEngine.UI;

public class InvisibleRandomSpawnerState : RandomSpawnerState
{
    public InvisibleRandomSpawnerState(IFactory factory, IStationStateSwitcher stateSwitcher, 
        Transform[] spawnBombPositions, Transform[] spawnWarnPositions, Transform[] spawnBonusPositions, 
        CurrentScore currentScore, Text statusText, BombsHandler bombsHandler) 
        : base(factory, stateSwitcher, spawnBombPositions, spawnWarnPositions, 
            spawnBonusPositions, currentScore, statusText, bombsHandler)
    {
    }

    public override async void Start()
    {
        _bombsHandler.BombDisabled += SpawnBonus;
        await ShowAndHideStatus("Invisible Random Spawn");
    }

    public override void Stop()
    {
        _bombsHandler.BombDisabled -= SpawnBonus;
    }

    public override void Spawn()
    {
        if (_currentScore.Amount % 37 == 0)
            _stateSwitcher.RandomSwitchState<RandomSpawnerState, TriggerSpawnerState>();
        
        if (RandomTime(0.35f, 1.25f))
            _factory.Spawn(_spawnBombPositions[RandomPosition()], GameContentType.Bomb);
    }
}
