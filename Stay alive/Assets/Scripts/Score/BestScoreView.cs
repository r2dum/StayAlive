using UnityEngine;
using UnityEngine.UI;

public class BestScoreView : MonoBehaviour
{
    [SerializeField] private Text[] _bestScoreTexts;

    private BestScore _bestScore;

    public void Initialize(BestScore bestScore)
    {
        _bestScore = bestScore;
        _bestScore.Changed += SetView;
        SetView(_bestScore.Amount);
    }

    private void OnDisable()
    {
        _bestScore.Changed -= SetView;
    }

    private void SetView(int record)
    {
        foreach (var bestScoreText in _bestScoreTexts)
        {
            bestScoreText.text = $"{record}";
        }
    }
}
