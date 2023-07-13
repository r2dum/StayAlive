using System;
using System.Collections;
using UnityEngine;

public class PlayerArmour : MonoBehaviour
{
    private int _lifeTime;
    
    private readonly WaitForSecondsRealtime _delay = 
        new WaitForSecondsRealtime(1f);
    
    public event Action<int> TimeChanged;
    public event Action<bool> Activated;
    
    public void Activate(int lifeTime)
    {
        _lifeTime = lifeTime;
        TimeChanged?.Invoke(_lifeTime);
        
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
        
        while (_lifeTime > 0)
        {
            TimeChanged?.Invoke(_lifeTime);
            _lifeTime -= delayStep;
            yield return _delay;
        }
        
        gameObject.SetActive(false);
        Activated?.Invoke(false);
    }
}
