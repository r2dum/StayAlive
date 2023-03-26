using UnityEngine;

public interface IFactory
{
    ISpawnable Spawn(Transform position, GameContentType type);
}