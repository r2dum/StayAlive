using System;
using UnityEngine;

public class Armour : MonoBehaviour, ISpawnable
{
    public event Action<ISpawnable> Disabled;
    
    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.TryGetComponent(out Player player))
        {
            Disabled?.Invoke(this);
            Destroy(gameObject);
        }
    }
    
    public void SetPause(bool isPaused)
    {
        //Add animation in the future
    }
}
