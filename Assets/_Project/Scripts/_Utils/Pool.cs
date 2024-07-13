using System;
using UnityEngine;
using UnityEngine.Pool;

public interface IPool<T>
{
    public T Get();
    public void ReleaseEntity(T obj);
}

public abstract class PoolBase<T> : MonoBehaviour, IPool<T> where T : Component
{
    protected ObjectPool<T> _pool;
    protected virtual bool CollectionCheck { get; } = true;
    protected virtual int DefaultCapacity { get; set; } = 5;
    protected virtual int MaxSize { get; } = 100;

    public virtual T Get()
    {
        return _pool.Get();
    }
    public void ReleaseEntity(T obj)
    {
        _pool.Release(obj);
    }
    protected void SetupInstance()
    {
        _pool = new ObjectPool<T>(CreateEntity, OnEntityGet, OnEntityRelease, OnEntityDestroy, CollectionCheck, DefaultCapacity, MaxSize);
    }
    protected abstract T CreateEntity();

    protected virtual Action<T> OnEntityGet { get; } = null;
    protected virtual Action<T> OnEntityRelease { get; } = null;
    protected virtual Action<T> OnEntityDestroy { get; } = null;

}