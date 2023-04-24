using UnityEngine;
using UnityEngine.UI;

public class CurrentScoreView : MonoBehaviour
{
    [SerializeField] private Text _currentScoreText;
    
    private CurrentScore _currentScore;

    public void Initialize(CurrentScore currentScore)
    {
        _currentScore = currentScore;
        Bomb.Dropped += OnScoreChanged;
        _currentScore.ScoreChanged += SetView;
    }

    public void OnDisable()
    {
        Bomb.Dropped -= OnScoreChanged;
        _currentScore.ScoreChanged -= SetView;
    }

    private void OnScoreChanged()
    {
        _currentScore.AddScore();
    }

    private void SetView(int score)
    { 
        _currentScoreText.text = $"{score}";
    }
}
