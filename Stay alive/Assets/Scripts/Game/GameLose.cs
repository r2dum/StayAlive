using UnityEngine;
using UnityEngine.UI;

public class GameLose : MonoBehaviour
{
    [SerializeField] private LosePanel _losePanel;
    [SerializeField] private CurrentScoreView _currentScoreView;
    [SerializeField] private Text _recordText;
    [SerializeField] private Text _collectedCoinsText;
    private int _saveWalletCoins;
    
    private Player _player;
    private Wallet _wallet;
    private BestScore _bestScore;
    private ISaveSystem _saveSystem;
    private GameData _gameData;
    
    public void Initialize(Player player, Wallet wallet, BestScore bestScore, 
        ISaveSystem saveSystem, GameData gameData, SceneLoader sceneLoader)
    {
        _player = player;
        _wallet = wallet;
        _bestScore = bestScore;
        _saveSystem = saveSystem;
        _gameData = gameData;
        _saveWalletCoins = _wallet.Coins;
        
        _losePanel.Initialize(sceneLoader);
        _player.Died += OnPlayerDied;
        _bestScore.Changed += OnBestScoreChanged;
    }
    
    private void OnDisable()
    {
        _player.Died -= OnPlayerDied;
        _bestScore.Changed -= OnBestScoreChanged;
    }
    
    private void OnPlayerDied()
    {
        _currentScoreView.enabled = false;
        _bestScore.TrySave();
        _gameData.WalletCoins = _wallet.Coins;
        _saveSystem.Save(_gameData);
        
        var collectedCoins = _wallet.Coins - _saveWalletCoins;
        _collectedCoinsText.text = $"+{collectedCoins}";
        _losePanel.Show();
    }
    
    private void OnBestScoreChanged(int record)
    {
        _recordText.text = "NEW RECORD!!!";
        _gameData.BestScore = record;
        _saveSystem.Save(_gameData);
    }
}
