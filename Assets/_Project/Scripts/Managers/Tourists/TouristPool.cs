using System;

public class TouristPool : PoolBase<Tourist>
{
    public int _usedTourists, _maxAmount;
    public bool HasFreeTourists => _usedTourists < _maxAmount;

    public void SetUp(int maxAmount)
    {
        _usedTourists = 0;
        _maxAmount = maxAmount;
        DefaultCapacity = maxAmount;
        SetupInstance();
    }

    protected override Tourist CreateEntity()
    {
        throw new System.NotImplementedException();
    }
    public Tourist Get(Path path, float speed, Resource resource, int amount) {
        _usedTourists++;
        var t = Get();
        t.Set(path, speed, resource, amount);
        return t;
    }
    protected override Action<Tourist> OnEntityRelease => (t) => _usedTourists--;
}