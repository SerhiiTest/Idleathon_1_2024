using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TouristManager
{
    private TouristPool _pool;

    private List<CityBuilding> _cityBuildings;

    private Path[] _paths;
    private int _pathSwitcher = 0;

    public TouristManagerStats BaseStats { get; private set; }
    private WallkerVisual[] _visuals;
    private int _visualSwitcher = 0;
    public TouristManager(List<CityBuilding> cityBuildings, TouristManagerStats stats, Path[] paths, WallkerVisual[] touristVisuals)
    {
        GameObject gameObj = new(typeof(TouristPool).Name);
        _pool = gameObj.AddComponent<TouristPool>();
        _pool.Init(stats.MaxAmount);
        //_pool.SetUp(stats.MaxAmount);

        _paths = paths;
        _visuals = touristVisuals;
        BaseStats = stats;

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
        BaseStats = GameManager.Instance.UpgradeCityStats(BaseStats, r.Stats,r.Level);
        _pool.SetUp(BaseStats.MaxAmount);
    }
    private float _timer;
    public void Update(float delta) {
        
        if(_pool.HasFreeTourists && _timer < 7 /*Replace with formula based on BaseSpeed & BaseSpeed level*/)
        {
            _timer = 0;

            int r = UnityEngine.Random.Range(0, 10);
            if (r > 6) {
                _pool.Get(_visuals[_visualSwitcher],_paths[_pathSwitcher], BaseStats.Speed, Resource.Sand, BaseStats.CarryWeight);
                // Add to list & subscribe for event
            }
            else
            {
                _pool.Get(_visuals[_visualSwitcher], _paths[_pathSwitcher], BaseStats.Speed, Resource.Money, BaseStats.Comfort);
                // Add to list & subscribe for event
            }
            _pathSwitcher = (_pathSwitcher + 1) % _paths.Length;
            _visualSwitcher = (_visualSwitcher+1) % _visuals.Length;
        }
        else
        {
            _timer += delta;
        }

        _pool.UpdateAllActive(delta);
    }

}
[Serializable]
public struct TouristManagerStats
{
    public int Speed;
    public int CarryWeight;
    public int MaxAmount;
    public int Comfort;

    public static TouristManagerStats operator +(TouristManagerStats a,TouristManagerStats b)
    {
        return new TouristManagerStats() { CarryWeight = a.CarryWeight + b.CarryWeight, Comfort = a.Comfort + b.Comfort, MaxAmount = a.MaxAmount + b.MaxAmount, Speed = a.Speed + b.Speed };
    }
}
