using UnityEngine;

public class WarnSpawnerFactory : MonoBehaviour
{
    [SerializeField] private Warn _warn;
    [SerializeField] private Transform[] _spawnWarnPosition;
    
    [SerializeField] private int _poolCount;
    [SerializeField] private Transform _container;
    [SerializeField] private bool _autoExpand;
    
    private PoolMono<Warn> _warnPool;
    
    private void Start()
    {
        _warnPool = new PoolMono<Warn>(_warn, _poolCount, _container);
        _warnPool.AutoExpand = _autoExpand;
    }
    /*
    public ISpawnable Spawn(Transform position, GameContentType type)
    {
        return _warnPool.GetFreeElement(_spawnWarnPosition);
    }*/
}
