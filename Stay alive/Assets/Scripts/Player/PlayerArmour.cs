using System;
using System.Collections;
using UnityEngine;

public class PlayerArmour : MonoBehaviour, IPauseHandler
{
    [SerializeField] private int _lifeTime;
    
    private int _currentLifeTime;
    private bool _isPaused;
    
    private readonly WaitForSecondsRealtime _delay = 
        new WaitForSecondsRealtime(1f);
    
    public event Action<int> TimeChanged;
    public event Action<bool> Activated;
    
    public void Activate()
    {
        _currentLifeTime = _lifeTime;
        TimeChanged?.Invoke(_currentLifeTime);
        
        if (gameObject.activeInHierarchy == false)
        {
            gameObject.SetActive(true);
            Activated?.Invoke(true);
            StartCoroutine(Deactivating());
        }
    }
    
    private IEnumerator Deactivating()
    {
        var delayStep = 1;
        
        while (_currentLifeTime > 0)
        {
            while (_isPaused)
            {
                yield return null;
            }
            
            TimeChanged?.Invoke(_currentLifeTime);
            _currentLifeTime -= delayStep;
            yield return _delay;
        }
        
        gameObject.SetActive(false);
        Activated?.Invoke(false);
    }
    
    public void SetPause(bool isPaused)
    {
        _isPaused = isPaused;
    }
}
