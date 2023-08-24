using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [Header("Items")]
    [SerializeField] private ShopItem[] _playerSkins;
    [SerializeField] private ShopItemMap[] _mapSkins;
    
    [Header("Items Shop UI")]
    [SerializeField] private Button _arrowLeftButton;
    [SerializeField] private Button _arrowRightButton;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _selectButton;
    [SerializeField] private Text _selectedText;
    [SerializeField] private Text _priceText;
    
    [Header("Global Shop UI")]
    [SerializeField] private Button _playersShopButton;
    [SerializeField] private Button _mapsShopButton;
    [SerializeField] private Button _exitShopButton;
    [SerializeField] private GameUIColorChanger _gameUIColorChanger;
    
    [SerializeField] private Vector3 _playerShopTarget;
    [SerializeField] private Vector3 _mapShopTarget;
    
    [Header("Wallet")]
    [SerializeField] private Wallet _wallet;
    [SerializeField] private WalletSoundWithUIView _walletView;
    
    private ShopData _shopData;
    private GameData _gameData;
    private ISaveSystem _shopSaveSystem;
    private ISaveSystem _gameSaveSystem;
    private SceneLoader _sceneLoader;
    private Camera _camera;
    
    private int _currentPlayerSkin;
    private int _currentMapSkin;
    
    private void Awake()
    {
        _camera = Camera.main;
        _shopData = new ShopData();
        _gameData = new GameData();
        _sceneLoader = new SceneLoader(new CleanUpHandler());
        _shopSaveSystem = new JsonSaveSystem(Constants.SHOP_DATA_PATH);
        _gameSaveSystem = new JsonSaveSystem(Constants.GAME_DATA_PATH);
        
        _shopData = _shopSaveSystem.Load(_shopData);
        _gameData = _gameSaveSystem.Load(_gameData);
        _wallet.Initialize(_gameData);
        _walletView.Initialize(_wallet);
        
        LoadItem(_shopData.CurrentPlayer, _playerSkins, ref _currentPlayerSkin);
        LoadItem(_shopData.CurrentMap, _mapSkins, ref _currentMapSkin);
        
        PlayersShop();
        GlobalCanvas();
    }
    
    private void LoadItem(string dataSkin, IReadOnlyList<ShopItem> skins, ref int currentSkin)
    {
        while (skins[currentSkin].name != dataSkin)
            currentSkin++;
        
        skins[currentSkin].gameObject.SetActive(true);
        
        if (skins == _mapSkins)
            LoadUIColor();
    }
    
    private void PlayersShop()
    {
        if (_shopData.CurrentPlayer == _playerSkins[_currentPlayerSkin].name)
            ItemIsSelected();
        
        else if (_shopData.CurrentPlayer != _playerSkins[_currentPlayerSkin].name)
            CheckHaveItem(_shopData.HavePlayers, _playerSkins, _currentPlayerSkin);
        
        _arrowLeftButton.onClick.AddListener(() => ArrowLeft(_shopData.HavePlayers, 
            _shopData.CurrentPlayer, _playerSkins, ref _currentPlayerSkin));
        _arrowRightButton.onClick.AddListener(() => ArrowRight(_shopData.HavePlayers, 
            _shopData.CurrentPlayer, _playerSkins, ref _currentPlayerSkin));
        _buyButton.onClick.AddListener(() => TryBuyItem(_shopData.HavePlayers, _playerSkins, _currentPlayerSkin));
        _selectButton.onClick.AddListener(() => SelectItem(out _shopData.CurrentPlayer, _playerSkins, _currentPlayerSkin));
        
        LoadButtons(_playerSkins, _currentPlayerSkin);
        _mapsShopButton.ChangeButtonAlpha(0.5f);
        _playersShopButton.ChangeButtonAlpha(1f);
        DOTween.Sequence()
            .Append(_camera.transform.DORotate(_playerShopTarget, 0.7f));
    }
    
    private void MapsShop()
    {
        if (_shopData.CurrentMap == _mapSkins[_currentMapSkin].name)
            ItemIsSelected();
        
        else if (_shopData.CurrentMap != _mapSkins[_currentMapSkin].name)
            CheckHaveItem(_shopData.HaveMaps, _mapSkins, _currentMapSkin);
        
        _arrowLeftButton.onClick.AddListener(() => ArrowLeft(_shopData.HaveMaps,
            _shopData.CurrentMap, _mapSkins, ref _currentMapSkin));
        _arrowRightButton.onClick.AddListener(() => ArrowRight(_shopData.HaveMaps,
            _shopData.CurrentMap, _mapSkins, ref _currentMapSkin));
        _buyButton.onClick.AddListener(() => TryBuyItem(_shopData.HaveMaps, _mapSkins, _currentMapSkin));
        _selectButton.onClick.AddListener(() => SelectItem(out _shopData.CurrentMap, _mapSkins, _currentMapSkin));
        
        LoadButtons(_mapSkins, _currentMapSkin);
        _playersShopButton.ChangeButtonAlpha(0.5f);
        _mapsShopButton.ChangeButtonAlpha(1f);
        DOTween.Sequence()
            .Append(_camera.transform.DORotate(_mapShopTarget, 0.7f));
    }

    private void GlobalCanvas()
    {
        _mapsShopButton.onClick.AddListener(OnMapsMenuButtonClicked);
        _exitShopButton.onClick.AddListener(OnExitShopButtonClicked);
    }
    
    private void CheckHaveItem(List<string> listDataSkins, IReadOnlyList<ShopItem> skins, int currentSkin)
    {
        foreach (var dataSkin in listDataSkins)
        {
            if (skins[currentSkin].name == dataSkin)
            {
                _selectedText.gameObject.SetActive(false);
                _buyButton.gameObject.SetActive(false);
                _selectButton.gameObject.SetActive(true);
                return;
            }
        }
        
        _selectButton.gameObject.SetActive(false);
        _selectedText.gameObject.SetActive(false);
        _buyButton.gameObject.SetActive(true);
        _priceText.text = $"{skins[currentSkin].Price}";
    }
    
    private void ArrowRight(List<string> listDataSkins, string dataSkin, IReadOnlyList<ShopItem> skins, ref int currentSkin)
    {
        if (currentSkin < skins.Count)
        {
            if (currentSkin == 0)
                _arrowLeftButton.gameObject.SetActive(true);

            skins[currentSkin].gameObject.SetActive(false);
            currentSkin++;
            skins[currentSkin].gameObject.SetActive(true);

            if (dataSkin == skins[currentSkin].name)
                ItemIsSelected();
            
            else if (dataSkin != skins[currentSkin].name)
                CheckHaveItem(listDataSkins, skins, currentSkin);

            if (currentSkin + 1 == skins.Count)
                _arrowRightButton.gameObject.SetActive(false);
        }
        
        if (skins == _mapSkins)
            LoadUIColor();
    }
    
    private void ArrowLeft(List<string> listDataSkins, string dataSkin, IReadOnlyList<ShopItem> skins, ref int currentSkin)
    {
        if (currentSkin < skins.Count)
        {
            skins[currentSkin].gameObject.SetActive(false);
            currentSkin--;
            skins[currentSkin].gameObject.SetActive(true);
            _arrowRightButton.gameObject.SetActive(true);
        
            if (dataSkin == skins[currentSkin].name)
                ItemIsSelected();
            
            else if (dataSkin != skins[currentSkin].name)
                CheckHaveItem(listDataSkins, skins, currentSkin);
            
            if (currentSkin == 0)
                _arrowLeftButton.gameObject.SetActive(false);
        }
        
        if (skins == _mapSkins)
            LoadUIColor();
    }

    private void SelectItem(out string dataSkin, IReadOnlyList<ShopItem> skins, int currentSkin)
    {
        dataSkin = skins[currentSkin].name;
        _shopSaveSystem.Save(_shopData);

        _selectButton.gameObject.SetActive(false);
        _selectedText.gameObject.SetActive(true);

        if (skins == _mapSkins)
        {
            _gameData.UIColor = _mapSkins[_currentMapSkin].Color;
            _gameData.BombType = _mapSkins[_currentMapSkin].BombType;
            _gameSaveSystem.Save(_gameData);
        }
    }
    
    private void ItemIsSelected()
    {
        _buyButton.gameObject.SetActive(false);
        _selectButton.gameObject.SetActive(false);
        _selectedText.gameObject.SetActive(true);
    }
    
    private void TryBuyItem(List<string> listDataSkins, IReadOnlyList<ShopItem> skins, int currentSkin)
    {
        if (_wallet.Coins < skins[currentSkin].Price) 
            return;
        
        _wallet.BuyForCoins(skins[currentSkin].Price);
        _gameData.WalletCoins = _wallet.Coins;
        listDataSkins.Add(skins[currentSkin].name);
        
        _shopSaveSystem.Save(_shopData);
        _gameSaveSystem.Save(_gameData);
        
        _buyButton.gameObject.SetActive(false);
        _selectButton.gameObject.SetActive(true);
    }
    
    private void OnPlayersMenuButtonClicked()
    {
        _mapsShopButton.onClick.AddListener(OnMapsMenuButtonClicked);
        RemoveAllButtonsListeners(_playersShopButton);
        
        PlayersShop();
    }
    
    private void OnMapsMenuButtonClicked()
    {
        _playersShopButton.onClick.AddListener(OnPlayersMenuButtonClicked);
        RemoveAllButtonsListeners(_mapsShopButton);
        
        MapsShop();
    }
    
    private void RemoveAllButtonsListeners(Button shopButton)
    {
        shopButton.onClick.RemoveAllListeners();
        _arrowLeftButton.onClick.RemoveAllListeners();
        _arrowRightButton.onClick.RemoveAllListeners();
        _buyButton.onClick.RemoveAllListeners();
        _selectButton.onClick.RemoveAllListeners();
    }
    
    private void LoadButtons(IReadOnlyCollection<ShopItem> skins, int currentSkin)
    {
        if (currentSkin == 0)
            _arrowLeftButton.gameObject.SetActive(false);
        
        if (currentSkin > 0)
            _arrowLeftButton.gameObject.SetActive(true);
        
        if (currentSkin >= 0)
            _arrowRightButton.gameObject.SetActive(true);

        if (currentSkin + 1 == skins.Count)
            _arrowRightButton.gameObject.SetActive(false);
    }
    
    private void LoadUIColor()
    {
        _gameUIColorChanger.Initialize(_mapSkins[_currentMapSkin].Color);
        _playersShopButton.ChangeButtonAlpha(0.5f);
    }
    
    private void OnExitShopButtonClicked()
    {
        if (_camera.transform.eulerAngles.x >= _mapShopTarget.x)
        {
            LoadMenu();
        }
        else
        {
            DOTween.Sequence()
                .Append(_camera.transform.DORotate(_mapShopTarget, 0.7f))
                .OnComplete(LoadMenu);
        }
    }
    
    private async void LoadMenu()
    {
        await _sceneLoader.Menu();
    }
}
