using UnityEngine;

public class GameStartup : MonoBehaviour
{
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private Game _game;
    [SerializeField] private GamePause _gamePause;
    [SerializeField] private GameSettings _gameSettings;
    [SerializeField] private GameUIColorChanger _gameUIColorChanger;
    
    private GameData _gameData;
    private ISaveSystem _gameSaveSystem;
    private PauseHandler _pauseHandler;
    private CleanUpHandler _cleanUpHandler;
    private SceneLoader _sceneLoader;

    private void Awake()
    {
        _gameData = new GameData();
        _gameSaveSystem = new JsonSaveSystem(Constants.GAME_DATA_PATH);
        _gameData = _gameSaveSystem.Load(_gameData);
        
        _pauseHandler = new PauseHandler();
        _cleanUpHandler = new CleanUpHandler();
        _sceneLoader = new SceneLoader(_cleanUpHandler);
        
        _mainMenu.Initialize(_sceneLoader);
        _game.Initialize(_gameData, _gameSaveSystem, _pauseHandler, _cleanUpHandler, _sceneLoader);
        _gamePause.Initialize(_pauseHandler);
        _gameUIColorChanger.Initialize(_gameData.UIColor);
        _gameSettings.Initialize(_gameData, _gameSaveSystem);
    }
}
