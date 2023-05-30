using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [Header("Players Shop UI")]
    [SerializeField] private Button _arrowToLeftPlayer;
    [SerializeField] private Button _arrowToRightPlayer;
    [SerializeField] private Button _buttonBuyPlayer;
    [SerializeField] private Button _buttonSelectPlayer;
    [SerializeField] private Text _selectedPlayerText;
    [SerializeField] private Text _pricePlayerText;
    [SerializeField] private Canvas _playersCanvas;
    [SerializeField] private ShopItem[] _players;
    private int _player;
    
    [Header("Maps Shop UI")]
    [SerializeField] private Button _arrowToLeftMap;
    [SerializeField] private Button _arrowToRightMap;
    [SerializeField] private Button _buttonBuyMap;
    [SerializeField] private Button _buttonSelectMap;
    [SerializeField] private Text _selectedMapText;
    [SerializeField] private Text _priceMapText;
    [SerializeField] private Canvas _mapsCanvas;
    [SerializeField] private ShopItem[] _maps;
    private int _map;
    
    [Header("Global Shop UI")]
    [SerializeField] private Button _playersCanvasButton;
    [SerializeField] private Button _mapsCanvasButton;
    [SerializeField] private Button _exitShopButton;
    
    [SerializeField] private GameUIColor _gameUIColor;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private WalletSoundWithUIView _walletView;
    
    private ShopData _shopData;
    private JsonSaveSystem _jsonSaveSystem;
    private PlayerPrefsSystem _playerPrefsSystem;
    private SceneLoader _sceneLoader;
    
    private void Start()
    {
        _shopData = new ShopData();
        _sceneLoader = new SceneLoader();
        _jsonSaveSystem = new JsonSaveSystem();
        _playerPrefsSystem = new PlayerPrefsSystem();
        
        _shopData = _jsonSaveSystem.Load(_shopData);
        _wallet.Initialize(_playerPrefsSystem);
        _walletView.Initialize(_wallet);
        
        PlayersCanvas();
        MapsCanvas();
        GlobalCanvas();
    }
    
    private void LoadPlayer()
    {
        while (_players[_player].name != _shopData.CurrentPlayer)
            _player++;

        _players[_player].gameObject.SetActive(true);
    }
    
    private void LoadMap()
    {
        while (_maps[_map].name != _shopData.CurrentMap)
            _map++;

        _maps[_map].gameObject.SetActive(true);
        _gameUIColor.Initialize(_maps[_map].GetComponent<Map>().Color);
    }
    
    private void PlayersCanvas()
    {
        LoadPlayer();
        
        if (_shopData.CurrentPlayer == _players[_player].name)
            SelectedPlayer();
        else if (_shopData.CurrentPlayer != _players[_player].name)
            CheckHavePlayer();

        _arrowToLeftPlayer.onClick.AddListener(ArrowLeft);
        _arrowToRightPlayer.onClick.AddListener(ArrowRight);
        _buttonBuyPlayer.onClick.AddListener(BuyPlayer);
        _buttonSelectPlayer.onClick.AddListener(SelectPlayer);

        if (_player >= 0)
            _arrowToRightPlayer.gameObject.SetActive(true);

        if (_player > 0)
            _arrowToLeftPlayer.gameObject.SetActive(true);

        if (_player + 1 == _players.Length)
            _arrowToRightPlayer.gameObject.SetActive(false);
    }
    
    private void MapsCanvas()
    {
        LoadMap();
        
        if (_shopData.CurrentMap == _maps[_map].name)
            SelectedMap();
        
        else if (_shopData.CurrentMap != _maps[_map].name)
            CheckHaveMap();

        _arrowToLeftMap.onClick.AddListener(ArrowLeftMap);
        _arrowToRightMap.onClick.AddListener(ArrowRightMap);
        _buttonBuyMap.onClick.AddListener(BuyMap);
        _buttonSelectMap.onClick.AddListener(SelectMap);
        
        if (_map >= 0)
            _arrowToRightMap.gameObject.SetActive(true);

        if (_map > 0)
            _arrowToLeftMap.gameObject.SetActive(true);

        if (_map + 1 == _maps.Length)
            _arrowToRightMap.gameObject.SetActive(false);
    }

    private void GlobalCanvas()
    {
        _playersCanvasButton.onClick.AddListener(OnPlayersMenuButtonClicked);
        _mapsCanvasButton.onClick.AddListener(OnMapsMenuButtonClicked);
        _exitShopButton.onClick.AddListener(OnExitShopButtonClicked);
    }
    
    private void CheckHavePlayer()
    {
        for (int i = 0; i < _shopData.HavePlayers.Count; i++)
        {
            if (_players[_player].name == _shopData.HavePlayers[i])
            {
                _selectedPlayerText.gameObject.SetActive(false);
                _buttonBuyPlayer.gameObject.SetActive(false);
                _buttonSelectPlayer.gameObject.SetActive(true);
                return;
            }
        }
        
        _buttonSelectPlayer.gameObject.SetActive(false);
        _selectedPlayerText.gameObject.SetActive(false);
        _buttonBuyPlayer.gameObject.SetActive(true);
        _pricePlayerText.text = $"{_players[_player].Price}";
    }
    
    private void CheckHaveMap()
    {
        for (int i = 0; i < _shopData.HaveMaps.Count; i++)
        {
            if (_maps[_map].name == _shopData.HaveMaps[i])
            {
                _selectedMapText.gameObject.SetActive(false);
                _buttonBuyMap.gameObject.SetActive(false);
                _buttonSelectMap.gameObject.SetActive(true);
                return;
            }
        }
        
        _buttonSelectMap.gameObject.SetActive(false);
        _selectedMapText.gameObject.SetActive(false);
        _buttonBuyMap.gameObject.SetActive(true);
        _priceMapText.text = $"{_maps[_map].Price}";
    }
    
    private void ArrowRight()
    {
        if (_player < _players.Length)
        {
            if (_player == 0)
                _arrowToLeftPlayer.gameObject.SetActive(true);

            _players[_player].gameObject.SetActive(false);
            _player++;
            _players[_player].gameObject.SetActive(true);

            if (_shopData.CurrentPlayer == _players[_player].name)
                SelectedPlayer();
            
            else if (_shopData.CurrentPlayer != _players[_player].name)
                CheckHavePlayer();

            if (_player + 1 == _players.Length)
                _arrowToRightPlayer.gameObject.SetActive(false);
        }
    }

    private void ArrowLeft()
    {
        if (_player < _players.Length)
        {
            _players[_player].gameObject.SetActive(false);
            _player--;
            _players[_player].gameObject.SetActive(true);
            _arrowToRightPlayer.gameObject.SetActive(true);

            if (_shopData.CurrentPlayer == _players[_player].name)
                SelectedPlayer();

            else if (_shopData.CurrentPlayer != _players[_player].name)
                CheckHavePlayer();

            if (_player == 0)
                _arrowToLeftPlayer.gameObject.SetActive(false);
        }
    }
    
    private void ArrowRightMap()
    {
        if (_map < _maps.Length)
        {
            if (_map == 0)
                _arrowToLeftMap.gameObject.SetActive(true);

            _maps[_map].gameObject.SetActive(false);
            _map++;
            _maps[_map].gameObject.SetActive(true);
            _gameUIColor.Initialize(_maps[_map].GetComponent<Map>().Color);

            if (_shopData.CurrentMap == _maps[_map].name)
                SelectedMap();
                
            else if (_shopData.CurrentMap != _maps[_map].name)
                CheckHaveMap();

            if (_map + 1 == _maps.Length)
                _arrowToRightMap.gameObject.SetActive(false);
        }
    }

    private void ArrowLeftMap()
    {
        if (_map < _maps.Length)
        {
            _maps[_map].gameObject.SetActive(false);
            _map--;
            _maps[_map].gameObject.SetActive(true);
            _arrowToRightMap.gameObject.SetActive(true);
            _gameUIColor.Initialize(_maps[_map].GetComponent<Map>().Color);

            if (_shopData.CurrentMap == _maps[_map].name)
                SelectedMap();
            
            else if (_shopData.CurrentMap != _maps[_map].name)
                CheckHaveMap();

            if (_map == 0)
                _arrowToLeftMap.gameObject.SetActive(false);
        }
    }

    private void SelectPlayer()
    {
        _shopData.CurrentPlayer = _players[_player].name;
        _jsonSaveSystem.Save(_shopData);

        _buttonSelectPlayer.gameObject.SetActive(false);
        _selectedPlayerText.gameObject.SetActive(true);
    }

    private void BuyPlayer()
    {
        if (_wallet.Coins >= _players[_player].Price)
        {
            _wallet.BuyForCoins(_players[_player].Price);
            _shopData.HavePlayers.Add(_players[_player].name);
            _jsonSaveSystem.Save(_shopData);

            _buttonBuyPlayer.gameObject.SetActive(false);
            _buttonSelectPlayer.gameObject.SetActive(true);
        }
    }

    private void SelectMap()
    {
        _shopData.CurrentMap = _maps[_map].name;
        _jsonSaveSystem.Save(_shopData);

        _buttonSelectMap.gameObject.SetActive(false);
        _selectedMapText.gameObject.SetActive(true);
    }

    private void BuyMap()
    {
        if (_wallet.Coins >= _maps[_map].Price)
        {
            _wallet.BuyForCoins(_maps[_map].Price);
            _shopData.HaveMaps.Add(_maps[_map].name);
            _jsonSaveSystem.Save(_shopData);

            _buttonBuyMap.gameObject.SetActive(false);
            _buttonSelectMap.gameObject.SetActive(true);
        }
    }
    
    private void SelectedPlayer()
    {
        _buttonBuyPlayer.gameObject.SetActive(false);
        _buttonSelectPlayer.gameObject.SetActive(false);
        _selectedPlayerText.gameObject.SetActive(true);
    }

    private void SelectedMap()
    {
        _buttonBuyMap.gameObject.SetActive(false);
        _buttonSelectMap.gameObject.SetActive(false);
        _selectedMapText.gameObject.SetActive(true);
    }
    
    private void OnPlayersMenuButtonClicked()
    {
        _playersCanvas.enabled = true;
        _mapsCanvas.enabled = false;
    }
    
    private void OnMapsMenuButtonClicked()
    {
        _playersCanvas.enabled = false;
        _mapsCanvas.enabled = true;
    }
    
    private void OnExitShopButtonClicked()
    {
        _sceneLoader.Menu();
    }
}
