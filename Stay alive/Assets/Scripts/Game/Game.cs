using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Player[] _playerPrefabs;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Map[] _mapPrefabs;
    [SerializeField] private GameUIColor _gameUIColor;
    [SerializeField] private GameLose _gameLose;
    [SerializeField] private GamePause _gamePause;
    [SerializeField] private GameContentFactory _gameContentFactory;
    [SerializeField] private SpawnerStation _spawnerStation;
    [SerializeField] private WalletSoundWithUIView _walletView;
    [SerializeField] private CurrentScoreView _currentScoreView;
    [SerializeField] private BestScoreView _bestScoreView;
    [SerializeField] private Canvas _gameCanvas;
    [SerializeField] private int _gameFps;
    
    private Player _player;
    private Map _map;
    private ShopData _shopData;
    private Wallet _wallet;
    private CurrentScore _currentScore;
    private BestScore _bestScore;
    private BombsHandler _bombsHandler;
    private WarnsHandler _warnsHandler;
    private BonusesHandler _bonusesHandler;
    private PauseHandler _pauseHandler;
    private JsonSaveSystem _jsonSaveSystem;
    private PlayerPrefsSystem _playerPrefsSystem;
    
    public void Initialize(SceneLoader sceneLoader)
    {
        SetGameFps(_gameFps);
        SaveSystems();
        ShopData();
        PauseHandler();
        LoadPlayer();
        Wallet();
        Input();
        LoadMap();
        CurrentScore();
        BestScore();
        GameContentFactory();
        BombsHandler();
        WarnsHandler();
        BonusesHandler();
        SpawnerStation();
        View(sceneLoader);
        SetPause(true);
    }
    
    public void BeginGame()
    {
        _map.DestroyPlayButton();
        _spawnerStation.StartCurrentState();
        _gameCanvas.enabled = true;
        SetPause(false);
    }
    
    private void SetGameFps(int fps)
    {
        Application.targetFrameRate = fps;
    }
    
    private void SaveSystems()
    {
        _jsonSaveSystem = new JsonSaveSystem();
        _playerPrefsSystem = new PlayerPrefsSystem();
    }
    
    private void ShopData()
    {
        _shopData = new ShopData();
        _shopData = _jsonSaveSystem.Load(_shopData);
    }
    
    private void PauseHandler()
    {
        _pauseHandler = new PauseHandler();
        _gamePause.Initialize(_pauseHandler);
    }
    
    private void SetPause(bool isPaused)
    {
        _pauseHandler.SetPause(isPaused);
    }
    
    private void LoadPlayer()
    {
        foreach (var playerPrefab in _playerPrefabs)
        {
            if (playerPrefab.name == _shopData.CurrentPlayer)
            {
                var player = Instantiate(playerPrefab);
                _player = player;
                _pauseHandler.AddToPauseList(_player);
            }
        }
    }
    
    private void LoadMap()
    {
        foreach (var mapPrefab in _mapPrefabs)
        {
            if (mapPrefab.name == _shopData.CurrentMap)
            {
                var map = Instantiate(mapPrefab);
                _map = map;
            }
        }
    }
    
    private void Wallet()
    {
        _wallet = _player.GetComponent<Wallet>();
        _wallet.Initialize(_playerPrefsSystem);
    }
    
    private void Input()
    {
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            var mobileInput = new MobileInput();
            _playerInput.Initialize(_player, mobileInput);
        }
        else
        {
            var mouseInput = new MouseInput();
            _playerInput.Initialize(_player, mouseInput);
        }
        
        _pauseHandler.AddToPauseList(_playerInput);
    }
    
    private void CurrentScore()
    {
        _currentScore = new CurrentScore();
    }

    private void BestScore()
    {
        _bestScore = new BestScore(_currentScore, _playerPrefsSystem);
    }
    
    private void GameContentFactory()
    {
        _gameContentFactory.Initialize(_map.BombType);
    }
    
    private void SpawnerStation()
    {
        _spawnerStation.Initialize(_gameContentFactory, _currentScore, _bombsHandler);
        _pauseHandler.AddToPauseList(_spawnerStation);
    }
    
    private void BombsHandler()
    {
        _bombsHandler = new BombsHandler(_gameContentFactory);
        _pauseHandler.AddToPauseList(_bombsHandler);
    }
    
    private void WarnsHandler()
    {
        _warnsHandler = new WarnsHandler(_gameContentFactory);
        _pauseHandler.AddToPauseList(_warnsHandler);
    }
    
    private void BonusesHandler()
    {
        _bonusesHandler = new BonusesHandler(_gameContentFactory);
        _pauseHandler.AddToPauseList(_bonusesHandler);
    }
    
    private void View(SceneLoader sceneLoader)
    {
        _gameUIColor.Initialize(_map.Color);
        _walletView.Initialize(_wallet);
        _currentScoreView.Initialize(_currentScore, _bombsHandler);
        _bestScoreView.Initialize(_bestScore);
        _gameLose.Initialize(_player, _wallet, _bestScore, 
            _playerPrefsSystem, _currentScoreView, sceneLoader);
    }
}
