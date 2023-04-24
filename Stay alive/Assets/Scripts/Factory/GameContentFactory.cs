using System;
using UnityEngine;

public class GameContentFactory : MonoBehaviour, IFactory
{
    [SerializeField] private Bomb[] _bombPrefabs;
    [SerializeField] private Warn _warnPrefab;

    [SerializeField] private Transform _bombsContainer;
    [SerializeField] private Transform _warnsContainer;

    [SerializeField] private int _poolCount;
    [SerializeField] private bool _autoExpand;

    private PoolMono<Bomb> _bombsPool;
    private PoolMono<Warn> _warnsPool;

    public void Initialize(BombType bombType)
    {
        _bombsPool = new PoolMono<Bomb>(SetBombType(bombType), _poolCount, _bombsContainer);
        _bombsPool.AutoExpand = _autoExpand;

        _warnsPool = new PoolMono<Warn>(_warnPrefab, _poolCount, _warnsContainer);
        _warnsPool.AutoExpand = _autoExpand;
    }

    public ISpawnable Spawn(Transform position, GameContentType type)
    {
        switch (type)
        {
            case GameContentType.Bomb:
                return _bombsPool.GetFreeElement(position);
            case GameContentType.Warn:
                return _warnsPool.GetFreeElement(position);
            default:
                throw new Exception("Invalid product type.");
        }
    }

    private Bomb SetBombType(BombType type)
    {
        var prefab = _bombPrefabs[(int)type];
        return prefab;
    }
}
