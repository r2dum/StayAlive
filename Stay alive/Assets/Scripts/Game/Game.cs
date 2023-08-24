using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Player[] _playerPrefabs;
    [SerializeField] private PlayerArmour _playerArmourPrefab;
    [SerializeField] private Map[] _mapPrefabs;
    [SerializeField] private GameContentFactory _gameContentFactory;
    [SerializeField] private SpawnerStation _spawnerStation;
    [SerializeField] private GameView _gameView;
    [SerializeField] private GameLose _gameLose;
    [SerializeField] private Canvas _gameCanvas;
    
    private Player _player;
    private PlayerInput _playerInput;
    private PlayerArmour _playerArmour;
    private Map _map;
    private ShopData _shopData;
    private GameData _gameData;
    private Wallet _wallet;
    private CurrentScore _currentScore;
    private BestScore _bestScore;
    private CleanUpHandler _cleanUpHandler;
    private BombsHandler _bombsHandler;
    private WarnsHandler _warnsHandler;
    private BonusesHandler _bonusesHandler;
    private PauseHandler _pauseHandler;
    private ISaveSystem _shopSaveSystem;
    private ISaveSystem _jsonGameSaveSystem;
    
    public void Initialize(GameData gameData, ISaveSystem saveSystem, PauseHandler pauseHandler, 
        CleanUpHandler cleanUpHandler, SceneLoader sceneLoader)
    {
        Handlers(pauseHandler, cleanUpHandler);
        GameSaves(gameData, saveSystem);
        PlayerPrefab();
        PlayerInput();
        PlayerComponents();
        MapPrefab();
        CurrentScore();
        BestScore();
        GameContentFactory();
        BombsHandler();
        WarnsHandler();
        BonusesHandler();
        SpawnerStation();
        GameLose(sceneLoader);
        GameView();
        SetPause(true);
    }
    
    public void BeginGame(bool animatedButton)
    {
        _map.DestroyPlayButton(animatedButton);
        _spawnerStation.StartCurrentState();
        _gameCanvas.enabled = true;
        SetPause(false);
    }
    
    private void Handlers(PauseHandler pauseHandler, CleanUpHandler cleanUpHandler)
    {
        _pauseHandler = pauseHandler;
        _cleanUpHandler = cleanUpHandler;
    }
    
    private void GameSaves(GameData gameData, ISaveSystem saveSystem)
    {
        _gameData = gameData;
        _jsonGameSaveSystem = saveSystem;
        
        _shopData = new ShopData();
        _shopSaveSystem = new JsonSaveSystem(Constants.SHOP_DATA_PATH);
        _shopData = _shopSaveSystem.Load(_shopData);
    }
    
    private void SetPause(bool isPaused)
    {
        _pauseHandler.SetPause(isPaused);
    }
    
    private void PlayerPrefab()
    {
        foreach (var playerPrefab in _playerPrefabs)
        {
            if (playerPrefab.name == _shopData.CurrentPlayer)
            {
                _player = Instantiate(playerPrefab);
                _pauseHandler.AddToPauseList(_player);
            }
        }
    }
    
    private void MapPrefab()
    {
        foreach (var mapPrefab in _mapPrefabs)
        {
            if (mapPrefab.name == _shopData.CurrentMap)
            {
                _map = Instantiate(mapPrefab);
            }
        }
    }
    
    private void PlayerInput()
    {
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            var mobileInput = new MobileInput();
            _playerInput = new PlayerInput(_player, mobileInput);
        }
        else
        {
            var mouseInput = new MouseInput();
            _playerInput = new PlayerInput(_player, mouseInput);
        }
        
        _cleanUpHandler.AddToCleanList(_playerInput);
    }
    
    private void PlayerComponents()
    {
        _wallet = _player.GetComponent<Wallet>();
        _wallet.Initialize(_gameData);
        _playerArmour = Instantiate(_playerArmourPrefab, _player.transform, false);
        _pauseHandler.AddToPauseList(_playerArmour);
        _player.Initialize(_playerInput, _playerArmour);
    }
    
    private void CurrentScore()
    {
        _currentScore = new CurrentScore();
    }

    private void BestScore()
    {
        _bestScore = new BestScore(_currentScore, _gameData);
    }
    
    private void GameContentFactory()
    {
        _gameContentFactory.Initialize(_gameData.BombType);
    }
    
    private void SpawnerStation()
    {
        _spawnerStation.Initialize(_gameContentFactory, _bombsHandler, _cleanUpHandler);
        _pauseHandler.AddToPauseList(_spawnerStation);
    }
    
    private void BombsHandler()
    {
        _bombsHandler = new BombsHandler(_gameContentFactory);
        _pauseHandler.AddToPauseList(_bombsHandler);
        _cleanUpHandler.AddToCleanList(_bombsHandler);
    }
    
    private void WarnsHandler()
    {
        _warnsHandler = new WarnsHandler(_gameContentFactory);
        _pauseHandler.AddToPauseList(_warnsHandler);
        _cleanUpHandler.AddToCleanList(_warnsHandler);
    }
    
    private void BonusesHandler()
    {
        _bonusesHandler = new BonusesHandler(_gameContentFactory);
        _pauseHandler.AddToPauseList(_bonusesHandler);
        _cleanUpHandler.AddToCleanList(_bonusesHandler);
    }
    
    private void GameLose(SceneLoader sceneLoader)
    {
        _gameLose.Initialize(_player, _wallet, _bestScore, _jsonGameSaveSystem, _gameData, sceneLoader);
    }
    
    private void GameView()
    {
        _gameView.Initialize(_gameData.BombType, _wallet, _playerArmour, _currentScore, _bestScore, _bombsHandler); 
    }
}
