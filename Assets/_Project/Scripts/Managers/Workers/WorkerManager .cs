using System.Collections.Generic;

public class WorkerManager
{
    private WorkerPool _pool;

    private List<WorkerBuilding> _workerBuildings;

    private List<Ruin> _ruins;
    private List<int> _needReconstructionIds = new();
    private float _timerToAutoAction = 0;
    public WorkerManagerStats BaseStats { get; private set; }

    public WorkerManager(List<Ruin> ruins, List<WorkerBuilding> workerBuildings, WorkerManagerStats stats)
    {
        _pool = new WorkerPool();
        _pool.SetUp(stats.MaxAmount);

        BaseStats = stats;
        foreach (var ruin in ruins)
        {
            ruin.OnUpgrade += OnRuinLevelUp;
        }
        foreach (var building in workerBuildings)
        {
            building.OnUpgrade += OnRuinLevelUp;
        }

        _ruins = ruins;
        _workerBuildings = workerBuildings;
    }

    private void OnRuinLevelUp(int id)
    {
        Ruin r = _ruins[id];
        if (r.IsMaxed)
        {
            _needReconstructionIds.Remove(id);
            // Send Finished Event
        }
        // Upgrade Comfort
    }
    private void OnBuildingLevelUp(int id)
    {
        WorkerBuilding b = _workerBuildings[id];
        if (b.IsMaxed)
        {
            // Send Finished Event
        }
        BaseStats = GameManager.UpgradeWorkerStats(BaseStats, b.Stats);
    }

    public void Update(float delta)
    {
        if(true /*Replace with settings of autoconstruct*/ && _needReconstructionIds.Count == 0)
        {
            if(_timerToAutoAction >= GameManager.Instance.GameConfig.TimeToAutoAction)
            {
                _timerToAutoAction = 0;
                int tempID = -1;
                int tempMinimumCost = int.MaxValue;
                
                for(int i=0; i < _ruins.Count; i++)
                {
                    if (_ruins[i].IsMaxed)
                        continue;
                    if (_ruins[i].PriceToUpgrade < tempMinimumCost)
                    {
                        tempMinimumCost = _ruins[i].PriceToUpgrade;
                        tempID = i;
                    }
                }
                if (tempID != -1)
                {
                    _needReconstructionIds.Add(tempID);
                }
            }
            else
            {
                _timerToAutoAction += delta;
            }

            if(_pool.HasFreeWorkers && GameManager.Instance.Sand > 0 && _needReconstructionIds.Count > 0)
            {
                _pool.Get(_ruins[_needReconstructionIds[0]].Path, BaseStats.Speed, GameManager.Instance.TryGet(Resource.Sand, BaseStats.CarryWeight));
                // Add to list & subscribe for event
            }
        }

        // Update Workers
    }

}

public struct WorkerManagerStats
{
    public int Speed;
    public int CarryWeight;
    public int MaxAmount;
}
