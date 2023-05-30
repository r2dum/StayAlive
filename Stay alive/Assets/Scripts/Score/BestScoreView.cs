using UnityEngine;
using UnityEngine.UI;

public class BestScoreView : MonoBehaviour
{
    [SerializeField] private Text[] _bestScoreText;

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
        for (int i = 0; i < _bestScoreText.Length; i++)
        {
            _bestScoreText[i].text = $"{record}";
        }
    }
}
