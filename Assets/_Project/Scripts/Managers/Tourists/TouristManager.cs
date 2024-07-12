using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouristManager
{
    private TouristPool _pool;

    private List<Building> _cityBuildings;

    public TouristManagerStats BaseStats { get; private set; }

    public TouristManager(List<Building> cityBuildings)
    {
        _pool = new();
        foreach (var b in cityBuildings)
        {
            b.OnUpgrade += OnBuildingLevelUp;
        }
        _cityBuildings = cityBuildings;
    }

    private void OnBuildingLevelUp(int id)
    {
        Building r = _cityBuildings[id];
        if (r.IsMaxed)
        {
            _cityBuildings.RemoveAt(id);
            // Send Finished Event
        }
        // UpgradeStats
    }
}
public struct TouristManagerStats
{
    public int Speed;
    public int CarryWeight;
    public int MaxAmount;
    public int Comfort;
}
