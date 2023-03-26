using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [Header("Moving")]
    [SerializeField] private GameObject _mesh;
    
    [SerializeField] private float _timeForMove = 0.35f;
    [SerializeField] private float _jumpHeight = 1.0f;
    
    [SerializeField] private bool _canMove = true;
    [SerializeField] private bool _isMove;
    
    [Header("Particle")]
    [SerializeField] private ParticleSystem _particle;
    
    [Header("Audio")]
    [SerializeField] private AudioSource _jumpSound;
    
    private float _elapsedTime;
    private float _startY;
    
    private Vector3 _current;
    private Vector3 _target;
    
    private Rigidbody _rigidbody;
    
    public event Action Died;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        
        _current = transform.position;
        _startY = transform.position.y;
    }
    
    private void Update()
    {
        if (_canMove)
            HandleInput();
        
        if (_isMove)
            MovePlayer();
    }
    
    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.TryGetComponent(out Bomb bomb))
        {
            Instantiate(_particle, transform.position, Quaternion.identity);
            _mesh.SetActive(false);
            _canMove = false;
            Died?.Invoke();
        }
    }

    private void HandleInput()
    {
        if (SwipeManager.SwipeUp)
            Move(new Vector3(0, 0, 1.65f));
        
        if (SwipeManager.SwipeDown)
            Move(new Vector3(0, 0, -1.65f));
        
        if (SwipeManager.SwipeLeft)
            Move(new Vector3(-1.65f, 0, 0));
        
        if (SwipeManager.SwipeRight)
            Move(new Vector3(1.65f, 0, 0));
    }

    private void Move(Vector3 distance)
    {
        var newPosition = _current + distance;

        if (Physics.CheckSphere(newPosition + new Vector3(0.0f, 0.5f, 0.0f), 0.1f)) 
            return;

        _target = newPosition;
        _elapsedTime = 0;
        _canMove = false;
        _isMove = true;
        //_jumpSound.Play();

        switch (MoveDirection)
        {
            case "north":
                _mesh.transform.rotation = Quaternion.Euler(0, -90, 0);
                break;
            case "south":
                _mesh.transform.rotation = Quaternion.Euler(0, 90, 0);
                break;
            case "east":
                _mesh.transform.rotation = Quaternion.Euler(0, 180, 0);
                break;
            case "west":
                _mesh.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
        }
    }

    private void MovePlayer()
    {
        _elapsedTime += Time.deltaTime;

        var weight = (_elapsedTime < _timeForMove) ? (_elapsedTime / _timeForMove) : 1;
        var x = Lerp(_current.x, _target.x, weight);
        var z = Lerp(_current.z, _target.z, weight);
        var y = Sinerp(_current.y, _startY + _jumpHeight, weight);

        var result = new Vector3(x, y, z);
        transform.position = result;

        if (result == _target)
        {
            _canMove = true;
            _isMove = false;
            _current = _target;
            _rigidbody.AddForce(0, -10, 0, ForceMode.VelocityChange);
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

    private string MoveDirection
    {
        get
        {
            if (_isMove)
            {
                var dx = _target.x - _current.x;
                var dz = _target.z - _current.z;
                
                if (dz > 0)
                    return "north";
                
                else if (dz < 0)
                    return "south";
                
                else if (dx > 0)
                    return "west";
                
                else
                    return "east";
            }
            return null;
        }
    }
}