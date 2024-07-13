using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public GameConfig GameConfig { get; private set; }

    private TouristManager _touristManager;
    private WorkerManager _workerManager;

    public int Money;
    public int Sand;

    public int TryGet(Resource type, int amount)
    {
        if(type == Resource.Sand)
        {
            if(Sand > amount)
            {
                Sand -= amount;
                return amount;
            }
            else
            {
                amount = Sand;
                Sand = 0;
                return amount;
            }
        }
        else
        {
            if (Money > amount)
            {
                Money -= amount;
                return amount;
            }
            else
            {
                amount = Money;
                Money = 0;
                return amount;
            }
        }
    }

    #region Awake & OnDestroy
    void Awake()
    {
        Init();
    }
    private void OnDestroy()
    {
        Save();
    }
    #endregion

    public void Save() {}


    public void Init()
    {
        Instance = this;

        Sand = 0;
        Money = 0;

        List<Building> cityBuildings = new();
        List<Ruin> ruins = new();
        int temp = 0;
        foreach (var r in GameConfig.RuinsBuildings)
        {
            Ruin ruine = Instantiate(GameConfig.BuildingBasePrefab, transform, true).AddComponent<Ruin>();
            ruine.Set(temp, r.Item1,r.Item2);
            temp++;
            ruins.Add(ruine);
        }
        temp = 0;
        foreach(var c in GameConfig.CityBuildings)
        {
            Building ruine = Instantiate(GameConfig.BuildingBasePrefab, transform, true).AddComponent<Building>();
            ruine.Set(temp,c);
            temp++;
            cityBuildings.Add(ruine);
        }

        _touristManager = new(cityBuildings, GameConfig.touristManagerStats, GameConfig.TouristPaths);
        _workerManager = new(ruins, GameConfig.workerManagerStats);
    }

    private void FixedUpdate()
    {
        _touristManager.Update(Time.fixedDeltaTime);
        _workerManager.Update(Time.fixedDeltaTime);
    }

    public int GetUpgradeCost(int basePrice, int level) => GameConfig.GetNextPrice(basePrice, level);
}

// placeholder
public class Path
{

}
