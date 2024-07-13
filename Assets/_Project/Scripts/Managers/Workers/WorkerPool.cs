using System;
using System.Collections.Generic;

public class WorkerPool : PoolBase<Wallker>
{
    public int _usedWorkers, _maxAmount;
    public bool HasFreeWorkers => _usedWorkers < _maxAmount;

    private List<Wallker> _active = new();

    public void Init(int maxAmount)
    {
        _usedWorkers = 0;
        _maxAmount = maxAmount;
        DefaultCapacity = maxAmount;
        SetupInstance();
    }
    public void SetUp(int maxAmount)
    {
        _maxAmount = maxAmount;
    }

    protected override Wallker CreateEntity()
    {
        var w = Instantiate(GameManager.Instance.Config.WallkerPrefab, transform, true);
        w.OnFishish += () => ReleaseEntity(w);
        w.gameObject.SetActive(false);
        _active.Add(w);
        return w;
    }

    public Wallker Get(WallkerVisual wallkerVisual, Ruin ruin, float speed, int amount)
    {
        _usedWorkers++;
        var w = Get();
        w.Set(wallkerVisual, ruin.Path, speed);
        w.OnReachDestination += () => ruin.AddSand(amount);
        w.gameObject.SetActive(true);
        return w;
    }
    protected override Action<Wallker> OnEntityRelease => EntityRelease;
    private void EntityRelease(Wallker w)
    {
        w.gameObject.SetActive(false);
        _usedWorkers--;
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