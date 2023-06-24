using System;
using UnityEngine;

public class Coin : MonoBehaviour, ISpawnable
{
    public event Action<ISpawnable> Disabled;
    
    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.TryGetComponent(out Wallet wallet))
        {
            gameObject.SetActive(false);
            wallet.AddCoin();
            Disabled?.Invoke(this);
        }
    }
    
    public void SetPause(bool isPaused)
    {
        //Add animation in the future
    }
}
