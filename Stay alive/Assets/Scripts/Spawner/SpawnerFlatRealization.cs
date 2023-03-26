using System.Collections.Generic;
using UnityEngine;

public class SpawnerFlatRealization : MonoBehaviour
{
    [Header("Spawner Data")]
    [SerializeField] private Bomb[] _bombs;
    [SerializeField] private Transform[] _spawnBombPosition;
    [SerializeField] private GameObject[] _warns;
    [SerializeField] private GameObject[] _coins;
    
    [Header("Bombs Speed")]
    [SerializeField] private float _bombsSpeed;
    [SerializeField] private float _bombsSpeedIncrease;
    [SerializeField] private float _stopBombsSpeedIncrease;

    [Header("Pool Settings")]
    [SerializeField] private int _poolCount;
    [SerializeField] private Transform _container;
    [SerializeField] private bool _autoExpand;
    
    [Header("Random Spawn")]
    [SerializeField] private float _startSpawn;
    private float _timeSpawn;
    private int _randomPosition;

    private PoolMono<Bomb> _bombPool;
    private readonly List<GameObject> _warnsSpawn = new List<GameObject>();

    private void Start()
    {
        _bombPool = new PoolMono<Bomb>(_bombs[0], _poolCount, _container);
        _bombPool.AutoExpand = _autoExpand;
        _timeSpawn = _startSpawn;
    }

    private void OnEnable()
    {
        Bomb.Dropped += RemoveWarns;
    }

    private void OnDisable()
    {
        Bomb.Dropped -= RemoveWarns;
    }

    private void Update()
    {
        RandomTimeSpawn();
        RandomSpawnBomb();
        SpeedBomb();
    }

    public void SpawnBomb(int position)
    {
        if (_warns[position].activeInHierarchy)
            return;

        _warns[position].SetActive(true);
        _warnsSpawn.Add(_warns[position]);
        _bombPool.GetFreeElement(_spawnBombPosition[position]);
    }

    private void RandomSpawnBomb()
    {
        if (RandomTimeSpawn())
        {
            _randomPosition = Random.Range(0, _spawnBombPosition.Length);
            
            if (_warns[_randomPosition].activeInHierarchy)
                return;

            SpawnBomb(_randomPosition);
        }
    }

    private void RemoveWarns()
    {
        _warnsSpawn[0].SetActive(false);
        _warnsSpawn.RemoveAt(0);
        SpawnCoin();
    }

    private void SpeedBomb()
    {
        Bomb.Speed = _bombsSpeed;

        _bombsSpeed += _bombsSpeedIncrease * Time.deltaTime;

        if (_bombsSpeed >= _stopBombsSpeedIncrease)
        {
            _bombsSpeed = _stopBombsSpeedIncrease;
            _bombsSpeedIncrease = 0f;
        }
    }

    private void SpawnCoin()
    {
        if (Chance(13))
            _coins[_randomPosition].SetActive(true);
    }

    private bool RandomTimeSpawn()
    {
        if (_timeSpawn <= 0f)
        {
            _timeSpawn = Random.Range(0.35f, 1.25f);
            return true;
        }

        _timeSpawn -= Time.deltaTime;
        return false;
    }
    
    private bool Chance(int chance)
    {
        return Random.Range(0, 101) < chance;
    }
}
