using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Player[] _playerPrefabs;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Map[] _mapPrefabs;
    [SerializeField] private GameUIColor _gameUIColor;
    [SerializeField] private GameLose _gameLose;
    [SerializeField] private GameContentFactory _gameContentFactory;
    [SerializeField] private SpawnerStation _spawnerStation;
    [SerializeField] private WalletSoundWithUIView _walletView;
    [SerializeField] private CurrentScoreView _currentScoreView;
    [SerializeField] private BestScoreView _bestScoreView;

    private Player _player;
    private Map _map;
    private ShopData _shopData;
    private Wallet _wallet;
    private CurrentScore _currentScore;
    private BestScore _bestScore;
    
    private void Awake()
    {
        SetGameFps(120);
        ShopData();
        LoadPlayer();
        Wallet();
        Input();
        LoadMap();
        CurrentScore();
        BestScore();
        SpawnerStation();
        View();
    }
    
    private void SetGameFps(int fps)
    {
        Application.targetFrameRate = fps;  
    }
    
    private void ShopData()
    {
        _shopData = new ShopData();

        if (PlayerPrefs.HasKey("SaveGame"))
            _shopData = JsonUtility.FromJson<ShopData>(PlayerPrefs.GetString("SaveGame"));
        else
            PlayerPrefs.SetString("SaveGame", JsonUtility.ToJson(_shopData));
    }

    private void LoadPlayer()
    {
        foreach (var playerPrefab in _playerPrefabs)
        {
            if (playerPrefab.name == _shopData.CurrentPlayer)
            {
                var player = Instantiate(playerPrefab.gameObject);
                _player = player.GetComponent<Player>();
            }
        }
    }
    
    private void LoadMap()
    {
        foreach (var mapPrefab in _mapPrefabs)
        {
            if (mapPrefab.name == _shopData.CurrentMap)
            {
                var map = Instantiate(mapPrefab.gameObject);
                _map = map.GetComponent<Map>();
            }
        }
    }
    
    private void Wallet()
    {
        _wallet = _player.GetComponent<Wallet>();
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
    }
    
    private void CurrentScore()
    {
        _currentScore = new CurrentScore();
    }

    private void BestScore()
    {
        _bestScore = new BestScore(_currentScore);
    }

    private void SpawnerStation()
    {
        _gameContentFactory.Initialize(_map.BombType);
        _spawnerStation.Initialize(_gameContentFactory, _currentScore);
    }

    private void View()
    {
        _gameUIColor.Initialize(_map.Color);
        _walletView.Initialize(_wallet);
        _currentScoreView.Initialize(_currentScore);
        _bestScoreView.Initialize(_bestScore, _player);
        _gameLose.Initialize(_player);
    }
}
