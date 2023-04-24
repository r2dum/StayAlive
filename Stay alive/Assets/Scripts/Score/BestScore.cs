using System;
using UnityEngine;

public class BestScore
{
    public int Record => PlayerPrefs.GetInt("Record");
    
    private readonly CurrentScore _currentScore;
    
    public event Action<int> Changed;

    public BestScore(CurrentScore currentScore)
    {
        _currentScore = currentScore;
    }

    public void SetRecord()
    {
        if (_currentScore.Score > Record)
        {
            PlayerPrefs.SetInt("Record", _currentScore.Score);
            Changed?.Invoke(_currentScore.Score);
        }
    }
}
