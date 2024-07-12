using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerManager
{
    private WorkerPool _pool;

    private List<Ruin> _ruins;
    private List<int> _needReconstructionIds = new();

    public WorkerManagerStats BaseStats { get; private set; }

    public WorkerManager(List<Ruin> ruins)
    {
        _pool = new WorkerPool();
        foreach (var ruin in ruins)
        {
            ruin.OnUpgrade += OnBuildingLevelUp;
        }
        _ruins = ruins;
    }

    private void OnBuildingLevelUp(int id)
    {
        Ruin r = _ruins[id];
        if (r.IsMaxed)
        {
            _ruins.RemoveAt(id);
            _needReconstructionIds.Remove(id);
            // Send Finished Event
        }
        // UpgradeStats
    }
}

public struct WorkerManagerStats
{
    public int Speed;
    public int CarryWeight;
    public int MaxAmount;
}
