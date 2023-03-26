using UnityEngine;
using UnityEngine.UI;

public class BestScoreView : MonoBehaviour
{
    [SerializeField] private Text _bestScoreText;

    private BestScore _bestScore;
    
    public void Initialize()
    {
        UpdateView(_bestScore.Record);
    }

    /*private void OnEnable()
    {
        _bestScore.Enable();
        _bestScore.Changed += UpdateView;
    }*/

    /*private void OnDisable()
    {
        _bestScore.Disable();
        _bestScore.Changed -= UpdateView;
    }*/

    private void UpdateView(int record)
    {
        _bestScoreText.text = $"{record}";
    }
}
