using System;

public class CurrentScore
{
    public int Score { get; private set; }

    public event Action<int> ScoreChanged;
    
    public void AddScore()
    {
        Score++;
        ScoreChanged?.Invoke(Score);
    }
}
