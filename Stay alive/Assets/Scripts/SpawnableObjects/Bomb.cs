using System;
using UnityEngine;

public class Bomb : MonoBehaviour, ISpawnable, IPauseHandler
{
    [SerializeField] private float _speed = 9f;
    [SerializeField] private float _destroyPos = -3.8f;
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private BombType _type;
    
    private bool _isPaused;
    
    public event Action<Bomb> Dropped;
    
    private void Update()
    {
        if (_isPaused)
            return;
        
        transform.Translate(Vector3.down * (_speed * Time.deltaTime));

        if (transform.position.y <= _destroyPos)
        {
            Instantiate(_particle, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            Dropped?.Invoke(this);
        }
    }

    public void SetPause(bool isPaused)
    {
        _isPaused = isPaused;
    }
}
