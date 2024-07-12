using System;
using UnityEngine;
using UnityEngine.Pool;

public interface IPool<T>
{
    public T Get();
    public void ReleaseEntity(T obj);
}

public abstract class PoolBase<T> : MonoBehaviour, IPool<T> where T : Component, IReleasable
{
    protected ObjectPool<T> m_pool;
    protected virtual bool CollectionCheck { get; } = true;
    protected virtual int DefaultCapacity { get; } = 5;
    protected virtual int MaxSize { get; } = 100;

    protected virtual void Awake()
    {
        SetupInstance();
    }
    public virtual T Get()
    {
        return m_pool.Get();
    }
    public void ReleaseEntity(T obj)
    {
        m_pool.Release(obj);
    }
    private void SetupInstance()
    {
        m_pool = new ObjectPool<T>(CreateEntity, OnEntityGet, OnEntityRelease, OnEntityDestroy, CollectionCheck, DefaultCapacity, MaxSize);
    }
    protected abstract T CreateEntity();

    protected virtual Action<T> OnEntityGet { get; } = null;
    protected virtual Action<T> OnEntityRelease { get; } = null;
    protected virtual Action<T> OnEntityDestroy { get; } = null;

}