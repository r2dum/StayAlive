using UnityEngine;
using UnityEngine.UI;

public class LosePanel : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _mainMenuButton;
    
    private SceneLoader _sceneLoader;
    
    public void Initialize(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
        
        _restartButton.onClick.AddListener(OnRestartButtonClicked);
        _mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
    }
    
    private async void OnRestartButtonClicked()
    {
        await _sceneLoader.RestartGame();
    }
    
    private void OnMainMenuButtonClicked()
    {
        _sceneLoader.Menu();
    }
    
    public void Show()
    {
        gameObject.SetActive(true);
    }
}
