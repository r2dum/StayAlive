using UnityEngine;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameObject _menuCanvas;
    [SerializeField] private GameObject _gameCanvas;
    [SerializeField] private SpawnerStation _spawnerStation;

    public void StartGame()
    {
        _menuCanvas.SetActive(false);
        _gameCanvas.SetActive(true);
        _spawnerStation.enabled = true;
    }
}
