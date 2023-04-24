using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Wallet))]
public class Player : MonoBehaviour, IMovable
{
    [SerializeField] private GameObject _mesh;
    
    [SerializeField] private float _timeForMove = 0.35f;
    [SerializeField] private float _jumpHeight = 1.0f;
    
    [SerializeField] private bool _isMove;
    
    [SerializeField] private ParticleSystem _dieParticle;
    
    [SerializeField] private AudioSource _jumpSound;
    
    private float _elapsedTime;
    private float _startY;
    
    private Vector3 _currentPosition;
    private Vector3 _targetPosition;
    
    private Rigidbody _rigidbody;

    public event Action Died;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _currentPosition = transform.position;
        _startY = transform.position.y;
    }
    
    private void FixedUpdate()
    {
        if (_isMove)
            MovePlayer();
    }
    
    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.TryGetComponent(out Bomb bomb))
        {
            Died?.Invoke();
            Instantiate(_dieParticle, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }

    public void Move(Vector3 distance)
    {
        var newPosition = _currentPosition + distance;
        
        if (Physics.CheckSphere(newPosition + new Vector3(0.0f, 0.5f, 0.0f), 0.1f) || _isMove) 
            return;
        
        _targetPosition = newPosition;
        _elapsedTime = 0;
        _isMove = true;
        _jumpSound.Play();
        
        switch (MoveDirection)
        {
            case Direction.North:
                _mesh.transform.rotation = Quaternion.Euler(0, -90, 0);
                break;
            case Direction.South:
                _mesh.transform.rotation = Quaternion.Euler(0, 90, 0);
                break;
            case Direction.East:
                _mesh.transform.rotation = Quaternion.Euler(0, 180, 0);
                break;
            case Direction.West:
                _mesh.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
        }
    }

    private void MovePlayer()
    {
        _elapsedTime += Time.fixedDeltaTime;

        var weight = (_elapsedTime < _timeForMove) ? (_elapsedTime / _timeForMove) : 1;
        var x = Lerp(_currentPosition.x, _targetPosition.x, weight);
        var z = Lerp(_currentPosition.z, _targetPosition.z, weight);
        var y = Sinerp(_currentPosition.y, _startY + _jumpHeight, weight);

        var result = new Vector3(x, y, z);
        _rigidbody.MovePosition(result);

        if (result == _targetPosition)
        {
            _isMove = false;
            _currentPosition = _targetPosition;
        }
    }

    private float Lerp(float min, float max, float weight)
    {
        return min + (max - min) * weight;
    }

    private float Sinerp(float min, float max, float weight)
    {
        return min + (max - min) * Mathf.Sin(weight * Mathf.PI);
    }

    private Enum MoveDirection
    {
        get
        {
            if (_isMove)
            {
                var dx = _targetPosition.x - _currentPosition.x;
                var dz = _targetPosition.z - _currentPosition.z;
                
                if (dz > 0)
                    return Direction.North;
                
                else if (dz < 0)
                    return Direction.South;
                
                else if (dx > 0)
                    return Direction.West;
                
                else
                    return Direction.East;
            }
            return null;
        }
    }
}

public enum Direction
{
    North,
    South,
    West,
    East
}
