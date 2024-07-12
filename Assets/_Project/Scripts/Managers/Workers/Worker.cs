using System;
using UnityEngine;

public class Worker : MonoBehaviour, IReleasable
{
    public void Release()
    {
        throw new System.NotImplementedException();
    }

    internal void Set(Path path, float speed, int amount)
    {
        throw new NotImplementedException();
    }
}