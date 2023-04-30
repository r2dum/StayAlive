using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Canvas _menuCanvas;
    [SerializeField] private Canvas _gameCanvas;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private SpawnerStation _spawnerStation;

    private void Start()
    {
        _startGameButton.onClick.AddListener(OnStartGameButtonClicked);
        _playerInput.enabled = false;
        _spawnerStation.enabled = false;
    }

    private void OnStartGameButtonClicked()
    {
        _menuCanvas.gameObject.SetActive(false);
        _gameCanvas.gameObject.SetActive(true);
        _playerInput.enabled = true;
        _spawnerStation.enabled = true;
    }
}
