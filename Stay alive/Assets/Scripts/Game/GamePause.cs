using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class GamePause : MonoBehaviour
{
    [SerializeField] private PausePanel _pausePanel;
    [SerializeField] private LosePanel _losePanel;
    [SerializeField] private Canvas _menuCanvas;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Text _pauseTimerText;
    
    private PauseHandler _pauseHandler;
    
    public void Initialize(PauseHandler pauseHandler)
    {
        _pauseHandler = pauseHandler;
        _pauseButton.onClick.AddListener(OnPauseButtonClicked);
        _resumeButton.onClick.AddListener(OnResumeButtonClicked);
    }
    
    private void OnPauseButtonClicked()
    {
        _pausePanel.Show();
        _pauseButton.gameObject.SetActive(false);
        SetPause(true);
    }
    
    private async void OnResumeButtonClicked()
    {
        _pausePanel.Hide();
        _pauseTimerText.enabled = true;
        for (int i = 3; i >= 1; i--)
        {
            _pauseTimerText.text = $"{i}";
            await Task.Delay(1000);
        }
        _pauseTimerText.enabled = false;
        _pauseButton.gameObject.SetActive(true);
        SetPause(false);
    }
    
    private void OnApplicationPause(bool isPaused)
    {
        if (isPaused && _losePanel.gameObject.activeInHierarchy == false && 
            _menuCanvas.enabled == false)
            OnPauseButtonClicked();
    }
    
    private void SetPause(bool isPaused)
    {
        _pauseHandler.SetPause(isPaused);
    }
}
