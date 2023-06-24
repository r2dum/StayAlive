using UnityEngine;
using UnityEngine.UI;

public class CurrentScoreView : MonoBehaviour
{
    [SerializeField] private Text _currentScoreText;
    
    private CurrentScore _currentScore;
    private BombsHandler _bombsHandler;
    
    public void Initialize(CurrentScore currentScore, BombsHandler bombsHandler)
    {
        _currentScore = currentScore;
        _bombsHandler = bombsHandler;
        
        _bombsHandler.BombDisabled += OnBombDisabled;
        _currentScore.ScoreChanged += SetView;
    }
    
    private void OnDisable()
    {
        _bombsHandler.BombDisabled -= OnBombDisabled;
        _currentScore.ScoreChanged -= SetView;
    }
    
    private void OnBombDisabled(ISpawnable bomb, Transform position)
    {
        _currentScore.AddScore();
    }
    
    private void SetView(int score)
    { 
        _currentScoreText.text = $"{score}";
    }
}
