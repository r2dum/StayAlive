using System;
using UnityEngine;

public class TriggerBlock : MonoBehaviour
{
    [SerializeField] private int _position;

    public event Action<int> Triggered;

    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.TryGetComponent(out Player player))
        {
            Triggered?.Invoke(_position);
        }
    }
}
