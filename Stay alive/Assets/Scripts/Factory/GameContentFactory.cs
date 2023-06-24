using System;
using UnityEngine;

public class GameContentFactory : MonoBehaviour, IFactory
{
    [SerializeField] private Bomb[] _bombPrefabs;
    [SerializeField] private Warn _warnPrefab;
    [SerializeField] private Coin _coinPrefab;

    [SerializeField] private Transform _bombsContainer;
    [SerializeField] private Transform _warnsContainer;
    [SerializeField] private Transform _coinsContainer;

    [SerializeField] private int _poolCount;
    [SerializeField] private bool _autoExpand;

    private PoolMono<Bomb> _bombsPool;
    private PoolMono<Warn> _warnsPool;
    private PoolMono<Coin> _coinsPool;
    
    public event Action<ISpawnable, Transform> BombSpawned;
    public event Action<ISpawnable, Transform> WarnSpawned;
    public event Action<ISpawnable, Transform> BonusSpawned;
    
    public void Initialize(BombType bombType)
    {
        _bombsPool = new PoolMono<Bomb>(SetBombType(bombType), _poolCount, _bombsContainer);
        _bombsPool.AutoExpand = _autoExpand;

        _warnsPool = new PoolMono<Warn>(_warnPrefab, _poolCount, _warnsContainer);
        _warnsPool.AutoExpand = _autoExpand;

        _coinsPool = new PoolMono<Coin>(_coinPrefab, _poolCount, _coinsContainer);
        _coinsPool.AutoExpand = _autoExpand;
    }

    public ISpawnable Spawn(Transform position, GameContentType type)
    {
        if (position.gameObject.activeInHierarchy)
            return null;
        
        switch (type)
        {
            case GameContentType.Bomb:
                return _bombsPool.GetFreeElement(position, BombSpawned);
            case GameContentType.Warn:
                return _warnsPool.GetFreeElement(position, WarnSpawned);
            case GameContentType.Coin:
                return _coinsPool.GetFreeElement(position, BonusSpawned);
            default:
                throw new ArgumentException("Invalid product type.");
        }
    }

    private Bomb SetBombType(BombType type)
    {
        var prefab = _bombPrefabs[(int)type];
        return prefab;
    }
}
