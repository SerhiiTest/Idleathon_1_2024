public class TouristPool : PoolBase<Tourist>
{
    protected override Tourist CreateEntity()
    {
        throw new System.NotImplementedException();
    }
    public Tourist Get(Path path, float speed, Resource resource, int amount) {
        var t = Get();
        t.Set(path, speed, resource, amount);
        return t;
    }
}