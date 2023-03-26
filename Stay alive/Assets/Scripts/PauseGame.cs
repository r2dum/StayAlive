using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private GameObject _swipeManager;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private GameObject _pauseTimer;
    [SerializeField] private Text _pauseText;
    
    public void Resume()
    {
        StartCoroutine(ResumeTimer());
    }
    
    IEnumerator ResumeTimer()
    {
        _pausePanel.SetActive(false);
        _pauseTimer.SetActive(true);
        _pauseText.text = 3.ToString();
        yield return new WaitForSecondsRealtime(1f);
        _pauseText.text = 2.ToString();
        yield return new WaitForSecondsRealtime(1f);
        _pauseText.text = 1.ToString();
        yield return new WaitForSecondsRealtime(1f);
        _pauseTimer.SetActive(false);
        _pauseButton.SetActive(true);
        _swipeManager.SetActive(true);
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        _pausePanel.SetActive(true);
        _pauseButton.SetActive(false);
        _swipeManager.SetActive(false);
        Time.timeScale = 0f;
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            if (!_losePanel.activeInHierarchy)
            {
                _pausePanel.SetActive(true);
                _pauseButton.SetActive(false);
                _swipeManager.SetActive(false);
                Time.timeScale = 0;
            }
        }
    }
}
