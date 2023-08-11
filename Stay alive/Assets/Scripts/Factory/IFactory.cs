using System;
using UnityEngine;

public interface IFactory
{
    public event Action<ISpawnable, Transform> BombSpawned;
    public event Action<ISpawnable, Transform> WarnSpawned;
    public event Action<ISpawnable, Transform> BonusSpawned;
    
    ISpawnable Spawn(Transform position, GameContentType type);
}
