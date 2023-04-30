using System;
using UnityEngine;

public class BestScore
{
    private const string RECORDKEY = "Record";
    
    private readonly CurrentScore _currentScore;
    public int Record => PlayerPrefs.GetInt(RECORDKEY);
    
    public event Action<int> Changed;

    public BestScore(CurrentScore currentScore)
    {
        _currentScore = currentScore;
    }

    public void SetRecord()
    {
        if (_currentScore.Score > Record)
        {
            PlayerPrefs.SetInt(RECORDKEY, _currentScore.Score);
            Changed?.Invoke(_currentScore.Score);
        }
    }
}
