using UnityEngine;

public class GameStartup : MonoBehaviour
{
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private Game _game;
    
    private SceneLoader _sceneLoader;

    private void Awake()
    {
        _sceneLoader = new SceneLoader();
        _mainMenu.Initialize(_sceneLoader);
        _game.Initialize(_sceneLoader);
    }
}
