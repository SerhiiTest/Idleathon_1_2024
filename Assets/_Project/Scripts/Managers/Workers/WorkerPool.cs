using System;

public class WorkerPool : PoolBase<Worker>
{
    public int _usedWorkers, _maxAmount;

    public void SetUp(int maxAmount)
    {
        _usedWorkers = 0;
        _maxAmount = maxAmount;
        DefaultCapacity = maxAmount;
        SetupInstance();
    }

    public bool HasFreeWorkers => _usedWorkers < _maxAmount;

    protected override Worker CreateEntity()
    {
        throw new System.NotImplementedException();
    }

    public Worker Get(Path path, float speed, int amount)
    {
        _usedWorkers++;

        var w = Get();
        w.Set(path, speed, amount);
        return w;
    }
    protected override Action<Worker> OnEntityRelease => (w) => _usedWorkers--;
}