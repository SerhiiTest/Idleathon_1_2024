using System.Collections.Generic;
using UnityEngine;

public class TouristManager
{
    private TouristPool _pool;

    private List<CityBuilding> _cityBuildings;

    private Path[] _paths;
    private int _pathSwitcher = 0;

    public TouristManagerStats BaseStats { get; private set; }

    public TouristManager(List<CityBuilding> cityBuildings, TouristManagerStats stats, Path[] paths)
    {
        _pool = new ();
        _pool.SetUp(stats.MaxAmount);

        _paths = paths;

        foreach (var b in cityBuildings)
        {
            b.OnUpgrade += OnBuildingLevelUp;
        }
        _cityBuildings = cityBuildings;
    }

    private void OnBuildingLevelUp(int id)
    {
        CityBuilding r = _cityBuildings[id];
        if (r.IsMaxed)
        {
            _cityBuildings.RemoveAt(id);
            // Send Finished Event
        }

        BaseStats = GameManager.UpgradeCityStats(BaseStats, r.Stats);
    }
    private float _timer;
    public void Update(float delta) {
        
        if(_pool.HasFreeTourists && _timer < 7 /*Replace with formula based on BaseSpeed & BaseSpeed level*/)
        {
            _timer = 0;

            int r = Random.Range(0, 10);
            if (r > 6) {
                _pool.Get(_paths[_pathSwitcher], BaseStats.Speed, Resource.Sand, BaseStats.CarryWeight + r-8);
                // Add to list & subscribe for event
            }
            else
            {
                _pool.Get(_paths[_pathSwitcher], BaseStats.Speed, Resource.Money, BaseStats.Comfort /*REPLACE WITH FORMULA*/);
                // Add to list & subscribe for event
            }
        }
        else
        {
            _timer += delta;
        }


        // Update Tourists
    }

}
public struct TouristManagerStats
{
    public int Speed;
    public int CarryWeight;
    public int MaxAmount;
    public int Comfort;
}
