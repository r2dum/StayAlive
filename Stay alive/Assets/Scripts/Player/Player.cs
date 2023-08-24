using System;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Wallet))]
public class Player : MonoBehaviour, IMovable, IPauseHandler
{
    [SerializeField] private Transform _mesh;
    
    [SerializeField] private float _timeForMove = 0.35f;
    [SerializeField] private float _jumpHeight = 1.0f;
    
    [SerializeField] private bool _isMove;
    
    [SerializeField] private ParticleSystem _dieParticle;
    
    [SerializeField] private AudioSource _jumpSound;
    
    private float _elapsedTime;
    private float _startY;
    
    private bool _isPaused;
    
    private Vector3 _currentPosition;
    private Vector3 _targetPosition;
    
    private PlayerInput _playerInput;
    private PlayerArmour _playerArmour;
    private Rigidbody _rigidbody;

    public event Action Died;

    public void Initialize(PlayerInput playerInput, PlayerArmour playerArmour)
    {
        _playerInput = playerInput;
        _playerArmour = playerArmour;
        
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
        
        _currentPosition = transform.position;
        _startY = transform.position.y;
    }
    
    private void Update()
    {
        if (_isPaused)
            return;
        
        if (_isMove)
            MovePlayer();
        
        _playerInput.Update();
    }
    
    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.TryGetComponent(out Armour armour))
            _playerArmour.Activate();
        
        if (trigger.TryGetComponent(out Bomb bomb))
        {
            if (_playerArmour.gameObject.activeInHierarchy)
                return;
            
            Instantiate(_dieParticle, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            Died?.Invoke();
        }
    }
    
    public async void Move(Vector3 distance, Direction directionType)
    {
        while (_isMove)
        {
            await Task.Delay(1);
        }
        
        var newPosition = _currentPosition + distance;
        
        if (Physics.CheckSphere(newPosition + new Vector3(0.0f, 0.5f, 0.0f), 0.1f) || 
            _isPaused || gameObject.activeInHierarchy == false) 
            return;
        
        _targetPosition = newPosition;
        _elapsedTime = 0;
        _isMove = true;
        gameObject.transform.DOScale(new Vector3(1f, 0.85f, 1f), 0.25f);
        _jumpSound.Play();
        
        switch (directionType)
        {
            case Direction.North:
                _mesh.DORotate(new Vector3(0, -90, 0), 0.15f);
                break;
            case Direction.South:
                _mesh.DORotate(new Vector3(0, 90, 0), 0.15f);
                break;
            case Direction.East:
                _mesh.DORotate(new Vector3(0, 180, 0), 0.15f);
                break;
            case Direction.West:
                _mesh.DORotate(new Vector3(0, 0, 0), 0.15f);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(directionType), directionType, null);
        }
    }

    private void MovePlayer()
    {
        _elapsedTime += Time.deltaTime;

        var weight = (_elapsedTime < _timeForMove) ? (_elapsedTime / _timeForMove) : 1;
        var x = Lerp(_currentPosition.x, _targetPosition.x, weight);
        var z = Lerp(_currentPosition.z, _targetPosition.z, weight);
        var y = Sinerp(_currentPosition.y, _startY + _jumpHeight, weight);

        var result = new Vector3(x, y, z);
        transform.position = result;

        if (result == _targetPosition)
        {
            _isMove = false;
            _currentPosition = _targetPosition;
            gameObject.transform.DOScale(new Vector3(1f, 1f, 1f), 0.25f);
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
    
    public void SetPause(bool isPaused)
    {
        _isPaused = isPaused;
    }
}
