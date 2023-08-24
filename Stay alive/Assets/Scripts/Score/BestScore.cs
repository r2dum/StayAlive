using System;

public class BestScore
{
    public int Amount { get; private set; }
    
    private readonly CurrentScore _currentScore;

    public event Action<int> Changed;

    public BestScore(CurrentScore currentScore, GameData gameData)
    {
        _currentScore = currentScore;
        Amount = gameData.BestScore;
    }

    public void TrySave()
    {
        if (_currentScore.Amount > Amount)
            Changed?.Invoke(_currentScore.Amount);
    }
}
