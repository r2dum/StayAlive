using UnityEngine;
using UnityEngine.UI;

public class GameLose : MonoBehaviour
{
    [SerializeField] private LosePanel _losePanel;
    [SerializeField] private Text _collectedCoinsText;
    [SerializeField] private Text _recordText;
    [SerializeField] private Button _pauseButton;
    
    private int _saveCoins;
    
    private Player _player;
    private Wallet _wallet;
    private BestScore _bestScore;
    private PlayerPrefsSystem _saveSystem;
    private CurrentScoreView _currentScoreView;
    
    public void Initialize(Player player, Wallet wallet, BestScore bestScore, 
        PlayerPrefsSystem saveSystem, CurrentScoreView currentScoreView, SceneLoader sceneLoader)
    {
        _player = player;
        _wallet = wallet;
        _bestScore = bestScore;
        _saveSystem = saveSystem;
        _currentScoreView = currentScoreView;
        _saveCoins = _wallet.Coins;
        
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
        _pauseButton.gameObject.SetActive(false);
        _bestScore.TrySave();
        _saveSystem.Save(Constants.CASH, _wallet.Coins);
        _losePanel.Show();
        
        var collectedCoins = _wallet.Coins - _saveCoins;
        _collectedCoinsText.text = $"+{collectedCoins}";
    }
    
    private void OnBestScoreChanged(int record)
    {
        _recordText.text = "NEW RECORD!!!";
        _saveSystem.Save(Constants.RECORD, record);
    }
}
