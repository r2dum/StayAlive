using System;
using UnityEngine;

public class Bomb : MonoBehaviour, ISpawnable
{
    [SerializeField] private float _speed = 9f;
    [SerializeField] private float _destroyPos = -3.8f;
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private BombType _type;

    public static event Action Dropped;

    private void Update()
    {
        transform.Translate(Vector3.down * (_speed * Time.deltaTime));

        if (transform.position.y <= _destroyPos)
        {
            Instantiate(_particle, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            Dropped?.Invoke();
        }
    }
}
