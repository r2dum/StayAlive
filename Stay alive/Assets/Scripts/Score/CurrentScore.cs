using System;

public class CurrentScore
{
    public int Amount { get; private set; }

    public event Action<int> ScoreChanged;
    
    public void AddScore()
    {
        Amount++;
        ScoreChanged?.Invoke(Amount);
    }
}
