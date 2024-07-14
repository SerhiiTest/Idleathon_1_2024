using System;
using System.Collections.Generic;
using UnityEngine;

public class WorkerManager
{
    private WorkerPool _pool;

    private List<WorkerBuilding> _workerBuildings;

    private List<Ruin> _ruins;
    private List<int> _needReconstructionIds = new();
    private float _timerToAutoAction = 0;
    public WorkerManagerStats BaseStats { get; private set; }
    
    private WallkerVisual[] _visuals;
    private int _visualSwitcher = 0;

    private int FinishedRuins;
    public string RuinsInfo => $"{FinishedRuins}/{_ruins.Count}";

    public WorkerManager(List<Ruin> ruins, List<WorkerBuilding> workerBuildings, WorkerManagerStats stats, WallkerVisual[] workerVisuals)
    {
        GameObject gameObj = new(typeof(WorkerPool).Name);
        _pool = gameObj.AddComponent<WorkerPool>();
        _pool.Init(stats.MaxAmount);
        //_pool.SetUp(stats.MaxAmount);
        

        _visuals = workerVisuals;
        BaseStats = stats;
        foreach (var ruin in ruins)
        {
            ruin.OnUpgrade += OnRuinLevelUp;
        }
        foreach (var building in workerBuildings)
        {
            building.OnUpgrade += OnBuildingLevelUp;
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
            FinishedRuins++;
            // Send Finished Event
        }
        Debug.Log("TODO Add more comfort");
    }
    private void OnBuildingLevelUp(int id)
    {
        WorkerBuilding b = _workerBuildings[id];
        if (b.IsMaxed)
        {
            // Send Finished Event
        }
        BaseStats = GameManager.Instance.UpgradeWorkerStats(BaseStats, b.Stats,b.Level);
        _pool.SetUp(BaseStats.MaxAmount);
    }

    public void Update(float delta)
    {
        if(true /*Replace with settings of autoconstruct*/ && _needReconstructionIds.Count == 0)
        {
            if(_timerToAutoAction >= GameManager.Instance.Config.TimeToAutoAction)
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
        }

        if (_pool.HasFreeWorkers && GameManager.Instance.Sand > 0 && _needReconstructionIds.Count > 0)
        {
            _pool.Get(_visuals[_visualSwitcher], _ruins[_needReconstructionIds[0]], BaseStats.Speed, GameManager.Instance.TryGetSand(BaseStats.CarryWeight));
            // Add to list & subscribe for event
            _visualSwitcher = (_visualSwitcher + 1) % _visuals.Length;
        }

        _pool.UpdateAllActive(delta);
    }

}
[Serializable]
public struct WorkerManagerStats
{
    public int Speed;
    public int CarryWeight;
    public int MaxAmount;

    public static WorkerManagerStats operator +(WorkerManagerStats a, WorkerManagerStats b)
    {
        return new WorkerManagerStats() { CarryWeight = a.CarryWeight + b.CarryWeight, MaxAmount = a.MaxAmount + b.MaxAmount, Speed = a.Speed + b.Speed };
    }
}
