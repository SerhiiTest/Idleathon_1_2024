using System;
using System.Collections.Generic;
public class TouristPool : PoolBase<Wallker>
{
    public int _usedTourists, _maxAmount;

    public bool HasFreeTourists => _usedTourists < _maxAmount;


    private List<Wallker> _active = new();

    public void SetUp(int maxAmount)
    {
        _maxAmount = maxAmount;
    }
    public void Init(int maxAmount)
    {
        _usedTourists= 0;
        _maxAmount = maxAmount;
        DefaultCapacity = maxAmount;
        SetupInstance();
    }
    protected override Wallker CreateEntity()
    {
        var w = Instantiate(GameManager.Instance.Config.WallkerPrefab, transform, true);
        w.OnFishish += () => ReleaseEntity(w);
        w.gameObject.SetActive(false);
        _active.Add(w);
        return w;
    }
    public Wallker Get(WallkerVisual wallkerVisual, Path path, float speed, Resource resource, int amount) {
        _usedTourists++;
        var w = Get();
        w.Set(wallkerVisual, path, speed);
        w.OnReachDestination += () => GameManager.Instance.AddResources(resource, amount);
        w.gameObject.SetActive(true);
        w.Actve = true;
        return w;
    }
    protected override Action<Wallker> OnEntityRelease => EntityRelease;
    private void EntityRelease(Wallker w)
    {
        w.Actve = false;
        w.gameObject.SetActive(false);
        _usedTourists--;
    }

    public void UpdateAllActive(float delta)
    {
        foreach (var w in _active)
        {
            if (w.IsActive)
            {
                w.Move(delta);
            }
        }
    }
}