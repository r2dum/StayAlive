using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerStation : MonoBehaviour, IStationStateSwitcher, IPauseHandler
{
    [SerializeField] private Transform[] _spawnBombPositions;
    [SerializeField] private Transform[] _spawnWarnPositions;
    [SerializeField] private Transform[] _spawnBonusPositions;
    [SerializeField] private TriggerBlock[] _triggerBlocks;
    [SerializeField] private Text _statusText;

    private BaseSpawnerState _currentSpawnerState;
    private List<BaseSpawnerState> _allSpawnerStates;
    
    private IFactory _factory;
    private BombsHandler _bombsHandler;
    private BonusSpawner _bonusSpawner;
    private CleanUpHandler _cleanUpHandler;
    
    private bool _isPaused;
    
    public void Initialize(IFactory factory, BombsHandler bombsHandler, CleanUpHandler cleanUpHandler)
    {
        _factory = factory;
        _bombsHandler = bombsHandler;
        _cleanUpHandler = cleanUpHandler;
        
        _allSpawnerStates = new List<BaseSpawnerState>()
        {
            new RandomSpawnerState(_factory, this, _spawnBombPositions, 
                _spawnWarnPositions, _statusText),
            new TriggerSpawnerState(_factory, this, _spawnBombPositions, 
                _spawnWarnPositions, _statusText, _triggerBlocks),
            new InvisibleRandomSpawnerState(_factory, this, _spawnBombPositions,
                _spawnWarnPositions, _statusText),
            new BrokenRandomSpawnerState(_factory, this, _spawnBombPositions, 
                _spawnWarnPositions, _statusText),
            new OneFreeBlockSpawnerState(_factory, this, _spawnBombPositions, 
                _spawnWarnPositions, _statusText)
        };
        
        _bonusSpawner = new BonusSpawner(_factory, _bombsHandler, _spawnBonusPositions, _spawnBombPositions);
        _cleanUpHandler.AddToCleanList(_bonusSpawner);
        SwitchState<RandomSpawnerState>();
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
        
        if (_currentSpawnerState == null)
        {
            _currentSpawnerState = state;
            return;
        }
        
        if (_currentSpawnerState is IStoppable stoppable)
            stoppable.Stop();
        
        state.Start();
        _currentSpawnerState = state;
    }

    public void RandomSwitchState<T1, T2>() where T1 : BaseSpawnerState where T2 : BaseSpawnerState
    {
        var randomState = Random.Range(0, 2);

        switch (randomState)
        {
            case 0:
                SwitchState<T1>();
                break;
            case 1:
                SwitchState<T2>();
                break;
        }
    }
    
    public void StartCurrentState()
    {
        _currentSpawnerState.Start();
    }
    
    public void SetPause(bool isPaused)
    {
        _isPaused = isPaused;
    }
}
