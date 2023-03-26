using UnityEngine;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] _playButtons;
    [SerializeField] private GameObject _menuCanvas;
    [SerializeField] private GameObject _gameCanvas;
    [SerializeField] private SwipeManager _swipeManager;
    [SerializeField] private SpawnerStation _spawnerStation;
    [SerializeField] private TriggerBombSpawn _triggerBombSpawn;
    
    public void StartGame()
    {
        //_playButtons[Installer.Map].SetActive(false);
        _menuCanvas.SetActive(false);
        _gameCanvas.SetActive(true);
        _swipeManager.enabled = true;
        _spawnerStation.enabled = true;
        _triggerBombSpawn.enabled = true;
    }
}