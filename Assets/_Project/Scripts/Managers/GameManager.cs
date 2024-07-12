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

    public Resource Money;
    public Resource Sand;



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

        _touristManager = new(cityBuildings);
        _workerManager = new(ruins);
    }

    public int GetUpgradeCost(int basePrice, int level) => GameConfig.GetNextPrice(basePrice, level);
}

// placeholder
public class Path
{

}
