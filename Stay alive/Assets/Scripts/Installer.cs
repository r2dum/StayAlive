using UnityEngine;

public class Installer : MonoBehaviour
{
    [SerializeField] private Player[] _players;
    [SerializeField] private Map[] _maps;
    [SerializeField] private GameContentFactory _gameContentFactory;
    [SerializeField] private GameUIColor _gameUIColor;
    [SerializeField] private PlayerLose _playerLose;
    [SerializeField] private SpawnerStation _spawnerStation;
    //[SerializeField] private CurrentScoreSetup _currentScoreSetup;
    
    private ShopData _shopData;
    private BestScore _bestScore;
    
    private void Awake()
    {
        SetGameFps(120);
        ShopData();
        LoadPlayer();
        LoadMap();
        SpawnerStation();
        //CurrentScore();
        //BestScore();
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
        foreach (var player in _players)
        {
            if (player.name == _shopData.CurrentPlayer)
            {
                Instantiate(player.gameObject);
                _playerLose.Initialize(player);
            }
        }
    }

    private void LoadMap()
    {
        foreach (var map in _maps)
        {
            if (map.name == _shopData.CurrentMap)
            {
                Instantiate(map.gameObject);
                _gameUIColor.Initialize(map.Color);
                _gameContentFactory.Initialize(map.BombType);
            }
        }
    }

    private void SpawnerStation()
    {
        _spawnerStation.Initialize();
    }

    private void CurrentScore()
    {
        //_currentScoreSetup.Initialize();
    }

    private void BestScore()
    {
        //_bestScore = new BestScore(_currentScoreSetup);
    }
}
