public class WorkerPool : PoolBase<Worker>
{
    protected override Worker CreateEntity()
    {
        throw new System.NotImplementedException();
    }

    public Worker Get(Path path, float speed, int amount)
    {
        var w = Get();
        w.Set(path, speed, amount);
        return w;
    }

}