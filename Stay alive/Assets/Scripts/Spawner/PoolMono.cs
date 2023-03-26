using System;
using UnityEngine;
using System.Collections.Generic;
using Object = UnityEngine.Object;

public class PoolMono<T> where T : MonoBehaviour
{
    public T Prefab { get; }
    public bool AutoExpand { get; set; }
    public Transform Container { get; }

    private List<T> _pool;

    public PoolMono(T prefab, int count)
    {
        Prefab = prefab;

        CreatePool(count);
    }

    public PoolMono(T prefab, int count, Transform container)
    {
        Prefab = prefab;
        Container = container;

        CreatePool(count);
    }

    private void CreatePool(int count)
    {
        _pool = new List<T>();

        for (int i = 0; i < count; i++)
            CreateObject();
    }

    private T CreateObject(bool isActiveByDefault = false)
    {
        var createdObject = Object.Instantiate(Prefab, Container);
        createdObject.gameObject.SetActive(isActiveByDefault);
        _pool.Add(createdObject);
        return createdObject;
    }

    private bool HasFreeElement(out T element)
    {
        foreach (var mono in _pool)
        {
            if (mono.gameObject.activeInHierarchy == false)
            {
                element = mono;
                return true;
            }
        }

        element = null;
        return false;
    }

    public T GetFreeElement()
    {
        if (HasFreeElement(out var element))
        {
            element.gameObject.SetActive(true);
            return element;
        }

        if (AutoExpand)
            return CreateObject(true);

        throw new Exception($"No free elements in pool of type {typeof(T)}");
    }

    public T GetFreeElement(Transform spawnPoint)
    {
        var element = GetFreeElement();
        element.transform.position = spawnPoint.position;
        return element;
    }
}
