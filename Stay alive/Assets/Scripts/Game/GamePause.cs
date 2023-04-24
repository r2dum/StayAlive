using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GamePause : MonoBehaviour
{
    [SerializeField] private PausePanel _pausePanel;
    [SerializeField] private LosePanel _losePanel;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Text _pauseTimerText;

    private WaitForSecondsRealtime _delay = new WaitForSecondsRealtime(1f);
    
    public void Pause()
    {
        _pausePanel.Show();
        _pauseButton.gameObject.SetActive(false);
        Time.timeScale = 0f;
    }
    
    public void Resume()
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
        Time.timeScale = 1f;
    }
    
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            if (_losePanel.gameObject.activeInHierarchy == false)
                Pause();
        }
    }
}
