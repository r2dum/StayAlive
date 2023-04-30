using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GamePause : MonoBehaviour
{
    [SerializeField] private PausePanel _pausePanel;
    [SerializeField] private LosePanel _losePanel;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Text _pauseTimerText;
    
    private readonly WaitForSecondsRealtime _delay = 
        new WaitForSecondsRealtime(1f);
    
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
    
    private void OnResumeButtonClicked()
    {
        StartCoroutine(ResumeWithTimer());
    }
    
    private IEnumerator ResumeWithTimer()
    {
        _pausePanel.Hide();
        _pauseTimerText.gameObject.SetActive(true);
        _pauseTimerText.text = "3";
        yield return _delay;
        _pauseTimerText.text = "2";
        yield return _delay;
        _pauseTimerText.text = "1";
        yield return _delay;
        _pauseTimerText.gameObject.SetActive(false);
        _pauseButton.gameObject.SetActive(true);
        SetPause(false);
    }
    
    private void OnApplicationPause(bool isPaused)
    {
        if (isPaused && _losePanel.gameObject.activeInHierarchy == false)
            OnPauseButtonClicked();
    }
    
    private void SetPause(bool isPaused)
    {
        _pauseHandler.SetPause(isPaused);
    }
}
