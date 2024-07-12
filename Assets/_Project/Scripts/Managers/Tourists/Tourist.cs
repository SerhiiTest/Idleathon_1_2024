using System;
using UnityEngine;

public class Tourist : MonoBehaviour, IReleasable
{
    public void Release()
    {
    }

    internal void Set(Path path, float speed, Resource resource, int amount)
    {
        throw new NotImplementedException();
    }
}