using System;
using UnityEngine;

public class TriggerBlock : MonoBehaviour
{
    public event Action<TriggerBlock> Triggered;
    
    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.TryGetComponent(out Player player))
            Triggered?.Invoke(this);
    }
}
