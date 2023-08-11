using System;
using UnityEngine;

public class Fader : MonoBehaviour
{
    private const string FADER_PATH = "Fader";
    
    [SerializeField] private Animator _animator;
    
    private static Fader _instance;
    
    public static Fader Instance
    {
        get
        {
            if (_instance == null)
            {
                var prefab = Resources.Load<Fader>(FADER_PATH);
                _instance = Instantiate(prefab);
                DontDestroyOnLoad(_instance.gameObject);
            }
            
            return _instance;
        }
    }
    
    public bool IsFading { get; private set; }
    
    private Action _fadedInCallBack;
    private Action _fadedOutCallBack;
    
    public void FadeInScreen(Action fadedInCallBack)
    {
        if (IsFading)
            return;

        IsFading = true;
        _fadedInCallBack = fadedInCallBack;
        _animator.SetBool("faded", true);
    }
    
    public void FadeOutScreen(Action fadedOutCallBack)
    {
        if (IsFading)
            return;

        IsFading = true;
        _fadedOutCallBack = fadedOutCallBack;
        _animator.SetBool("faded", false);
    }

    private void Handle_FadeInAnimationOver()
    {
        _fadedInCallBack?.Invoke();
        _fadedInCallBack = null;
        IsFading = false;
    }
    
    private void Handle_FadeOutAnimationOver()
    {
        _fadedOutCallBack?.Invoke();
        _fadedOutCallBack = null;
        IsFading = false;
    }
}
