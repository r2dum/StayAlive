using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseSpawnerState
{
    protected readonly IFactory _factory;
    protected readonly IStationStateSwitcher _stateSwitcher;
    protected readonly Transform[] _spawnBombPositions;
    protected readonly Transform[] _spawnWarnPositions;
    protected readonly CurrentScore _currentScore;
    private readonly Text _statusText;
    
    protected BaseSpawnerState(IFactory factory, IStationStateSwitcher stateSwitcher, 
        Transform[] spawnBombPositions, Transform[] spawnWarnPositions,
        CurrentScore currentScore, Text statusText)
    {
        _factory = factory;
        _stateSwitcher = stateSwitcher;
        _spawnBombPositions = spawnBombPositions;
        _spawnWarnPositions = spawnWarnPositions;
        _currentScore = currentScore;
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
    
    public abstract void Start();
    public abstract void Stop();

    public abstract void Spawn();
}
