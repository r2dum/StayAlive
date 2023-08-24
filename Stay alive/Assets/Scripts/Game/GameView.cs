using UnityEngine;

public class GameView : MonoBehaviour
{
    [SerializeField] private ScoreImageView _scoreImageView;
    [SerializeField] private CurrentScoreView _currentScoreView;
    [SerializeField] private BestScoreView _bestScoreView;
    [SerializeField] private WalletSoundWithUIView _walletView;
    [SerializeField] private PlayerArmourView _playerArmourView;
    
    public void Initialize(BombType bombType, Wallet wallet, PlayerArmour playerArmour,
        CurrentScore currentScore, BestScore bestScore, BombsHandler bombsHandler)
    {
        _scoreImageView.Initialize(bombType);
        _walletView.Initialize(wallet);
        _playerArmourView.Initialize(playerArmour);
        _currentScoreView.Initialize(currentScore, bombsHandler);
        _bestScoreView.Initialize(bestScore);
    }
}
