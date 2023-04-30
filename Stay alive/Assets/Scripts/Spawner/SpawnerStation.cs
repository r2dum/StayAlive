using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnerStation : MonoBehaviour, IStationStateSwitcher, IPauseHandler
{
    [SerializeField] private List<Transform> _spawnBombPositions;
    [SerializeField] private List<Transform> _spawnWarnPositions;
    [SerializeField] private TriggerBlock[] _triggerBlocks;

    private bool _isPaused;

    private BaseSpawnerState _currentSpawnerState;
    private List<BaseSpawnerState> _allSpawnerStates;

    private CurrentScore _currentScore;
    private IFactory _factory;
    
    public void Initialize(IFactory factory, CurrentScore currentScore)
    {
        _factory = factory;
        
        _allSpawnerStates = new List<BaseSpawnerState>()
        {
            new RandomSpawnerState(_factory, this, _spawnBombPositions, _spawnWarnPositions, currentScore),
            new InvisibleRandomSpawnerState(_factory, this, _spawnBombPositions, _spawnWarnPositions, currentScore),
            new TriggerSpawnerState(_factory, this, _spawnBombPositions, _spawnWarnPositions, currentScore, _triggerBlocks)
        };
        
        _currentSpawnerState = _allSpawnerStates[0];
    }

    private void Update()
    {
        if (_isPaused)
            return;
        
        _currentSpawnerState.Spawn();
    }

    public void SwitchState<T>() where T : BaseSpawnerState
    {
        var state = _allSpawnerStates.FirstOrDefault(s => s is T);
        _currentSpawnerState.Stop();
        state.Start();
        _currentSpawnerState = state;
    }

    public void SetPause(bool isPaused)
    {
        _isPaused = isPaused;
    }
}
