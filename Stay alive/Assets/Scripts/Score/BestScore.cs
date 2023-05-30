using System;

public class BestScore
{
    public int Amount { get; private set; }
    
    private readonly CurrentScore _currentScore;

    public event Action<int> Changed;

    public BestScore(CurrentScore currentScore, PlayerPrefsSystem saveSystem)
    {
        _currentScore = currentScore;
        Amount = saveSystem.Load(Constants.RECORD);
    }

    public void TrySave()
    {
        if (_currentScore.Amount > Amount)
            Changed?.Invoke(_currentScore.Amount);
    }
}
