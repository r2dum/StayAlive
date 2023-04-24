using UnityEngine;
using UnityEngine.UI;

public class BestScoreView : MonoBehaviour
{
    [SerializeField] private Text[] _bestScoreText;

    private BestScore _bestScore;
    private Player _player;
    
    public void Initialize(BestScore bestScore, Player player)
    {
        _bestScore = bestScore;
        _player = player;
        _player.Died += OnBestScoreChanged;
        _bestScore.Changed += SetView;
        SetView(_bestScore.Record);
    }

    private void OnDisable()
    {
        _player.Died -= OnBestScoreChanged;
        _bestScore.Changed -= SetView;
    }

    private void OnBestScoreChanged()
    {
        _bestScore.SetRecord();
    }

    private void SetView(int record)
    {
        for (int i = 0; i < _bestScoreText.Length; i++)
        {
            _bestScoreText[i].text = $"{record}";
        }
    }
}
