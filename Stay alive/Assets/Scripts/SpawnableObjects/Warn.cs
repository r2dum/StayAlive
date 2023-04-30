using System;
using UnityEngine;

public class Warn : MonoBehaviour, ISpawnable, IPauseHandler
{
    private Animator _animator;
    private bool _isPaused;
    
    public event Action<Warn> Disabled;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.speed = 1f;
    }
    
    //It is used in Animation Event (Warning.anim), to disable the object after the animation has finished
    public void Disable()
    {
        gameObject.SetActive(false);
        Disabled?.Invoke(this);
    }
    
    public void SetPause(bool isPaused)
    {
        _isPaused = isPaused;

        _animator.speed = (_isPaused ? 0f : 1f);
    }
}
