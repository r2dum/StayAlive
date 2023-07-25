using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bomb : MonoBehaviour, ISpawnable
{
    [SerializeField] private float _speed = 9f;
    [SerializeField] private float _disablePos = -3.8f;
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private BombType _type;
    
    private bool _isPaused;
    
    public event Action<ISpawnable> Disabled;
    
    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.TryGetComponent(out PlayerArmour playerArmour))
            Disable();
    }
    
    private void Update()
    {
        if (_isPaused)
            return;
        
        transform.Translate(Vector3.down * (_speed * Time.deltaTime));
        
        if (transform.position.y <= _disablePos)
            Disable();
    }
    
    private void Disable()
    {
        Instantiate(_particle, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
        Disabled?.Invoke(this);
    }

    public void SetPause(bool isPaused)
    {
        _isPaused = isPaused;
    }
}
