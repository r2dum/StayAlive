using UnityEngine;
using UnityEngine.UI;

public class GameLose : MonoBehaviour
{
    [SerializeField] private LosePanel _losePanel;
    [SerializeField] private Button _pauseButton;
    
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
    }
    
    private void OnBestScoreChanged(int record)
    {
        _saveSystem.Save(Constants.RECORD, record);
    }
}
