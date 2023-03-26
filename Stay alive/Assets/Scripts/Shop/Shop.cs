using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [Header("Characters Shop UI")]
    [SerializeField] private GameObject _arrowToLeftPlayer;
    [SerializeField] private GameObject _arrowToRightPlayer;
    [SerializeField] private GameObject _buttonBuyPlayer;
    [SerializeField] private GameObject _buttonSelectPlayer;
    [SerializeField] private GameObject _textSelectPlayer;
    [SerializeField] private GameObject _skinMenu;
    [SerializeField] private Text _priceTextPlayer;
    
    [Header("SelectCharacters")]
    [SerializeField] private ShopItem[] _players;

    private string _statusCheck;
    private int _check;
    private int _iPlayer;
    
    [Header("Map shop UI")]
    [SerializeField] private GameObject _arrowToLeftMap;
    [SerializeField] private GameObject _arrowToRightMap;
    [SerializeField] private GameObject _buttonBuyMap;
    [SerializeField] private GameObject _buttonSelectMap;
    [SerializeField] private GameObject _textSelectMap;
    [SerializeField] private GameObject _mapMenu;
    [SerializeField] private Text _priceTextMap;
    
    [Header("SelectMap")]
    [SerializeField] private ShopItem[] _maps;
    [SerializeField] private GameUIColor _gameUIColor;
    
    private string _statusCheckMap;
    private int _checkMap;
    private int _iMap;
    
    [Header("Other")]
    private Wallet _wallet;
    private ShopData _shopData;
    
    private void Start()
    {
        _wallet = new Wallet();
        _shopData = new ShopData();
        
        if (PlayerPrefs.HasKey("SaveGame"))
            _shopData = JsonUtility.FromJson<ShopData>(PlayerPrefs.GetString("SaveGame"));
        
        else
            PlayerPrefs.SetString("SaveGame", JsonUtility.ToJson(_shopData));

        // CHARACTERS
        StartCoroutine(LoadCharacter());
        
        if (_shopData.CurrentPlayer == _players[_iPlayer].name)
        {
            _buttonBuyPlayer.SetActive(false);
            _buttonSelectPlayer.SetActive(false);
            _textSelectPlayer.SetActive(true);
        }
        else if (_shopData.CurrentPlayer != _players[_iPlayer].name)
            StartCoroutine(CheckHaveCharacter());

        if (_iPlayer >= 0)
            _arrowToRightPlayer.SetActive(true);

        if (_iPlayer > 0)
            _arrowToLeftPlayer.SetActive(true);

        if (_iPlayer + 1 == _players.Length)
            _arrowToRightPlayer.SetActive(false);
        

        //MAPS
        StartCoroutine(LoadMap());
        
        if (_shopData.CurrentMap == _maps[_iMap].name)
        {
            _buttonBuyMap.SetActive(false);
            _buttonSelectMap.SetActive(false);
            _textSelectMap.SetActive(true);
        }
        else if (_shopData.CurrentMap != _maps[_iMap].name)
            StartCoroutine(CheckHaveMap());
        
        if (_iMap >= 0)
            _arrowToRightMap.SetActive(true);

        if (_iMap > 0)
            _arrowToLeftMap.SetActive(true);

        if (_iMap + 1 == _maps.Length)
            _arrowToRightMap.SetActive(false);
    }

    private IEnumerator LoadCharacter()
    {
        _iPlayer = 0;

        while (_players[_iPlayer].name != _shopData.CurrentPlayer)
            _iPlayer++;

        _players[_iPlayer].gameObject.SetActive(true);

        yield return null;
    }

    private IEnumerator LoadMap()
    {
        _iMap = 0;

        while (_maps[_iMap].name != _shopData.CurrentMap)
            _iMap++;

        _maps[_iMap].gameObject.SetActive(true);
        _gameUIColor.Initialize(_maps[_iMap].GetComponent<Map>().Color);

        yield return null;
    }

    public IEnumerator CheckHaveCharacter()
    {
        while(_statusCheck != "Check")
        {
            if (_shopData.HavePlayers.Count != _check)
            {
                if (_players[_iPlayer].name != _shopData.HavePlayers[_check])
                {
                    _check++;
                }
                else if (_players[_iPlayer].name == _shopData.HavePlayers[_check])
                {
                    _textSelectPlayer.SetActive(false);
                    _buttonBuyPlayer.SetActive(false);
                    _buttonSelectPlayer.SetActive(true);
                    _check = 0;
                    _statusCheck = "Check";
                }
            }
            else if (_shopData.HavePlayers.Count == _check)
            {
                _buttonSelectPlayer.SetActive(false);
                _textSelectPlayer.SetActive(false);
                _buttonBuyPlayer.SetActive(true);
                _priceTextPlayer.text = _players[_iPlayer].Price.ToString();
                _check = 0;
                _statusCheck = "Check";
            }
        }

        _statusCheck = "";

        yield return null;
    }

    public IEnumerator CheckHaveMap()
    {
        while (_statusCheckMap != "CheckMap")
        {
            if (_shopData.HaveMaps.Count != _checkMap)
            {
                if (_maps[_iMap].name != _shopData.HaveMaps[_checkMap])
                {
                    _checkMap++;
                }
                else if (_maps[_iMap].name == _shopData.HaveMaps[_checkMap])
                {
                    _textSelectMap.SetActive(false);
                    _buttonBuyMap.SetActive(false);
                    _buttonSelectMap.SetActive(true);
                    _checkMap = 0;
                    _statusCheckMap = "CheckMap";
                }
            }
            else if (_shopData.HaveMaps.Count == _checkMap)
            {
                _buttonSelectMap.SetActive(false);
                _textSelectMap.SetActive(false);
                _buttonBuyMap.SetActive(true);
                _priceTextMap.text = _maps[_iMap].Price.ToString();
                _checkMap = 0;
                _statusCheckMap = "CheckMap";
            }
        }

        _statusCheckMap = "";

        yield return null;
    }

    public void ArrowRight()
    {
        if (_iPlayer < _players.Length)
        {
            if (_iPlayer == 0)
                _arrowToLeftPlayer.SetActive(true);

            _players[_iPlayer].gameObject.SetActive(false);
            _iPlayer++;
            _players[_iPlayer].gameObject.SetActive(true);

            if (_shopData.CurrentPlayer == _players[_iPlayer].name)
            {
                _buttonBuyPlayer.SetActive(false);
                _buttonSelectPlayer.SetActive(false);
                _textSelectPlayer.SetActive(true);
            }
            else if (_shopData.CurrentPlayer != _players[_iPlayer].name)
                StartCoroutine(CheckHaveCharacter());

            if (_iPlayer + 1 == _players.Length)
                _arrowToRightPlayer.SetActive(false);
        }
    }

    public void ArrowLeft()
    {
        if (_iPlayer < _players.Length)
        {
            _players[_iPlayer].gameObject.SetActive(false);
            _iPlayer--;
            _players[_iPlayer].gameObject.SetActive(true);
            _arrowToRightPlayer.SetActive(true);

            if (_shopData.CurrentPlayer == _players[_iPlayer].name)
            {
                _buttonBuyPlayer.SetActive(false);
                _buttonSelectPlayer.SetActive(false);
                _textSelectPlayer.SetActive(true);
            }
            else if (_shopData.CurrentPlayer != _players[_iPlayer].name)
                StartCoroutine(CheckHaveCharacter());

            if (_iPlayer == 0)
                _arrowToLeftPlayer.SetActive(false);
        }
    }

    public void ArrowRightMap()
    {
        if (_iMap < _maps.Length)
        {
            if (_iMap == 0)
                _arrowToLeftMap.SetActive(true);

            _maps[_iMap].gameObject.SetActive(false);
            _iMap++;
            _maps[_iMap].gameObject.SetActive(true);
            _gameUIColor.Initialize(_maps[_iMap].GetComponent<Map>().Color);

            if (_shopData.CurrentMap == _maps[_iMap].name)
            {
                _buttonBuyMap.SetActive(false);
                _buttonSelectMap.SetActive(false);
                _textSelectMap.SetActive(true);
            }
            else if (_shopData.CurrentMap != _maps[_iMap].name)
                StartCoroutine(CheckHaveMap());

            if (_iMap + 1 == _maps.Length)
                _arrowToRightMap.SetActive(false);
        }
    }

    public void ArrowLeftMap()
    {
        if (_iMap < _maps.Length)
        {
            _maps[_iMap].gameObject.SetActive(false);
            _iMap--;
            _maps[_iMap].gameObject.SetActive(true);
            _arrowToRightMap.SetActive(true);
            _gameUIColor.Initialize(_maps[_iMap].GetComponent<Map>().Color);

            if (_shopData.CurrentMap == _maps[_iMap].name)
            {
                _buttonBuyMap.SetActive(false);
                _buttonSelectMap.SetActive(false);
                _textSelectMap.SetActive(true);
            }
            else if (_shopData.CurrentMap != _maps[_iMap].name)
                StartCoroutine(CheckHaveMap());

            if (_iMap == 0)
                _arrowToLeftMap.SetActive(false);
        }
    }

    public void SelectCharacter()
    {
        _shopData.CurrentPlayer = _players[_iPlayer].name;

        PlayerPrefs.SetString("SaveGame", JsonUtility.ToJson(_shopData));

        _buttonSelectPlayer.SetActive(false);
        _textSelectPlayer.SetActive(true);
    }

    public void BuyCharacter()
    {
        if (_wallet.Coins >= _players[_iPlayer].Price)
        {
            _shopData = JsonUtility.FromJson<ShopData>(PlayerPrefs.GetString("SaveGame"));

            _wallet.BuyWithCoins(_players[_iPlayer].Price);
            _shopData.HavePlayers.Add(_players[_iPlayer].name);

            PlayerPrefs.SetString("SaveGame", JsonUtility.ToJson(_shopData));

            _buttonBuyPlayer.SetActive(false);
            _buttonSelectPlayer.SetActive(true);
        }
    }

    public void SelectMap()
    {
        _shopData.CurrentMap = _maps[_iMap].name;

        PlayerPrefs.SetString("SaveGame", JsonUtility.ToJson(_shopData));

        _buttonSelectMap.SetActive(false);
        _textSelectMap.SetActive(true);
    }

    public void BuyMap()
    {
        if (_wallet.Coins >= _maps[_iMap].Price)
        {
            _shopData = JsonUtility.FromJson<ShopData>(PlayerPrefs.GetString("SaveGame"));

            _wallet.BuyWithCoins(_maps[_iMap].Price);
            _shopData.HaveMaps.Add(_maps[_iMap].name);

            PlayerPrefs.SetString("SaveGame", JsonUtility.ToJson(_shopData));

            _buttonBuyMap.SetActive(false);
            _buttonSelectMap.SetActive(true);
        }
    }

    public void Skin()
    {
        _skinMenu.SetActive(true);
        _mapMenu.SetActive(false);
    }

    public void Map()
    {
        _skinMenu.SetActive(false);
        _mapMenu.SetActive(true);
    }
}