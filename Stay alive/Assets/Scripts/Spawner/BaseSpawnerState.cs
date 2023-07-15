using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseSpawnerState
{
    protected readonly IFactory _factory;
    protected readonly IStationStateSwitcher _stateSwitcher;
    protected readonly Transform[] _spawnBombPositions;
    protected readonly Transform[] _spawnWarnPositions;
    
    private readonly Text _statusText;
    private float _stateTime;
    
    protected BaseSpawnerState(IFactory factory, IStationStateSwitcher stateSwitcher, 
        Transform[] spawnBombPositions, Transform[] spawnWarnPositions, Text statusText)
    {
        _factory = factory;
        _stateSwitcher = stateSwitcher;
        _spawnBombPositions = spawnBombPositions;
        _spawnWarnPositions = spawnWarnPositions;
        _statusText = statusText;
    }
    
    protected async Task ShowAndHideStatus(string status)
    {
        _statusText.text = $"{status}";
        _statusText.gameObject.SetActive(true);
        Time.timeScale = 0.7f;
        await Task.Delay(1000);
        Time.timeScale = 1f;
        _statusText.gameObject.SetActive(false);
    }

    protected void SetStateTime(float minTime, float maxTime)
    {
        _stateTime = Random.Range(minTime, maxTime);
    }
    
    protected bool StateTimeIsOver()
    {
        if (_stateTime <= 0f)
            return true;
        
        _stateTime -= Time.deltaTime;
        return false;
    }
    
    public abstract void Start();
    public abstract void Spawn();
}
