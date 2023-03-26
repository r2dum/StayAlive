using System;
using UnityEngine;

public class Bomb : MonoBehaviour, ISpawnable
{
    [SerializeField] private GameObject _particle;
    [SerializeField] private float _destroyPos = -3.8f;
    [SerializeField] private BombType _type;
    public static float Speed = 9f;

    public static event Action Dropped;

    private void Update()
    {
        transform.Translate(Vector3.down * (Speed * Time.deltaTime));

        if (transform.position.y <= _destroyPos)
        {
            Instantiate(_particle, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            Dropped?.Invoke();
        }
    }
}
