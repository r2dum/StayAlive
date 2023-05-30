using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private Button _playGameButton;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Canvas _menuCanvas;

    private SceneLoader _sceneLoader;

    public void Initialize(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
        _playGameButton.onClick.AddListener(OnPlayGameButtonClicked);
        _shopButton.onClick.AddListener(OnShopButtonClicked);
    }
    
    public void Disable()
    {
        _menuCanvas.enabled = false;
    }
    
    private void OnPlayGameButtonClicked()
    {
        Disable();
        _game.BeginGame();
    }
    
    private void OnShopButtonClicked()
    {
        _sceneLoader.Shop();
    }
}
