using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnerStation : MonoBehaviour, IStationStateSwitcher
{
    [SerializeField] private List<Transform> _spawnBombPositions;
    [SerializeField] private List<Transform> _spawnWarnPositions;

    private BaseSpawnerState _currentSpawnerState;
    private List<BaseSpawnerState> _allSpawnerStates;

    private IFactory _factory;
    
    public void Initialize()
    {
        _factory = GetComponent<IFactory>();
        
        _allSpawnerStates = new List<BaseSpawnerState>()
        {
            new RandomSpawnerState(_factory, this, _spawnBombPositions, _spawnWarnPositions)
        };
        
        _currentSpawnerState = _allSpawnerStates[0];
    }

    private void Update()
    {
        _currentSpawnerState.Spawn();
    }

    public void SwitchState<T>() where T : BaseSpawnerState
    {
        var state = _allSpawnerStates.FirstOrDefault(s => s is T);
        _currentSpawnerState.Stop();
        state.Start();
        _currentSpawnerState = state;
    }
}