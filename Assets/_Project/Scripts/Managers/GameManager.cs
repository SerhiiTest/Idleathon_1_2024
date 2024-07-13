using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [field: SerializeField] public GameConfig Config { get; private set; }

    private TouristManager _touristManager;
    private WorkerManager _workerManager;

    #region Resources
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
    public void AddResources(Resource type, int amount)
    {
        if (type == Resource.Sand)
        {
            Sand += amount;
        }
        else
        {
            Money += amount;
        }
    }
    #endregion


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

        List<CityBuilding> cityBuildings = new();
        List<WorkerBuilding> workerBuildings = new();
        List<Ruin> ruins = new();
        int temp = 0;
        foreach (var r in Config.RuinsBuildings)
        {
            Ruin ruine = Instantiate(Config.BuildingBasePrefab, transform, true).AddComponent<Ruin>();
            ruine.transform.position = r.Position;
            ruine.Set(temp, r.Item1,r.Item2);
            temp++;
            ruins.Add(ruine);
        }
        temp = 0;
        foreach(var c in Config.CityBuildings)
        {
            CityBuilding building = Instantiate(Config.BuildingBasePrefab, transform, true).AddComponent<CityBuilding>();
            building.transform.position = c.Position;
            building.Set(temp, c.Item1, c.Item2);
            temp++;
            cityBuildings.Add(building);
        }
        temp = 0;
        foreach (var c in Config.WorkerBuildings)
        {
            WorkerBuilding building = Instantiate(Config.BuildingBasePrefab, transform, true).AddComponent<WorkerBuilding>();
            building.transform.position = c.Position;
            building.Set(temp, c.Item1, c.Item2);
            temp++;
            workerBuildings.Add(building);
        }

        _touristManager = new(cityBuildings, Config.touristManagerStats, Config.TouristPaths, Config.WorkerVisuals);
        _workerManager = new(ruins, workerBuildings, Config.workerManagerStats, Config.WorkerVisuals);
    }

    private void FixedUpdate()
    {
        _touristManager.Update(Time.fixedDeltaTime);
        _workerManager.Update(Time.fixedDeltaTime);
    }

    public int GetUpgradeCost(int basePrice, int level) => Config.GetNextPrice(basePrice, level);

    public TouristManagerStats UpgradeCityStats(TouristManagerStats baseStats, TouristManagerStats stats,int level) => Config.UpgradeCityStats(baseStats, stats,level);

    public WorkerManagerStats UpgradeWorkerStats(WorkerManagerStats baseStats, WorkerManagerStats stats, int level) => Config.UpgradeWorkerBuildingsStats(baseStats, stats, level);
}