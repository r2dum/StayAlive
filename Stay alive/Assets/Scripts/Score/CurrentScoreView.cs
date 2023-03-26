using UnityEngine;
using UnityEngine.UI;

public class CurrentScoreView : MonoBehaviour
{
    [SerializeField] private Text _currentScoreText;
    
    public void SetView(int score)
    {
        _currentScoreText.text = $"{score}";
    }
}
