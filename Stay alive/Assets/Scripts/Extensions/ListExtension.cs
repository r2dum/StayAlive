using System.Collections.Generic;
using UnityEngine;

public static class ListExtension
{
    public static T RandomPosition<T>(this List<T> list)
    {
        var index = Random.Range(0, list.Count);
        return list[index];
    }
}