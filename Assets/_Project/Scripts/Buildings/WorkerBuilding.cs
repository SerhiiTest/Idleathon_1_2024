public class WorkerBuilding : BuildingBase
{
    public WorkerManagerStats Stats { get; private set; }

    public void Set(int id, UpgradableBuildingSO data, WorkerManagerStats stats)
    {
        Set(id, data);
        Stats = stats;
    }
}
