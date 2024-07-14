using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [field: SerializeField] public PlayerHandler PlayerHandler { get; private set; }

    [field: SerializeField] public GameConfig Config { get; private set; }

    private TouristManager _touristManager;
    private WorkerManager _workerManager;

    #region Resources
    public int Money { get; private set; }
    public int Sand {  get; private set; }

    public int TryGetSand(int amount)
    {
        if(Sand > amount)
        {
            Sand -= amount;
            PlayerHandler.UpdateResources(Money,Sand);
            return amount;
        }
        else
        {
            amount = Sand;
            Sand = 0;
            PlayerHandler.UpdateResources(Money, Sand);
            return amount;
        }
    }

    public bool GetMoney(int amount)
    {
        if (Money >= amount)
        {
            Money -= amount;
            PlayerHandler.UpdateResources(Money, Sand);
            return true;
        }
        return false;
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
        PlayerHandler.UpdateResources(Money, Sand);
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
            Ruin ruin = Instantiate(Config.BuildingBasePrefab, transform).AddComponent<Ruin>();
            ruin.transform.position = r.Position;
            ruin.transform.localRotation = Quaternion.Euler(r.Rotation);
            ruin.Set(temp, r.Item1,r.Item2);
            temp++;
            ruin.OnUpgrade += (i)=> { PlayerHandler.UpdateStats(_touristManager.BaseStats, _workerManager.BaseStats); };
            ruins.Add(ruin);
        }
        temp = 0;
        foreach(var c in Config.CityBuildings)
        {
            CityBuilding building = Instantiate(Config.BuildingBasePrefab, transform).AddComponent<CityBuilding>();
            building.transform.localPosition = c.Position;
            building.transform.localRotation = Quaternion.Euler(c.Rotation);
            building.Set(temp, c.Item1, c.Item2);
            temp++;
            building.OnUpgrade  += (i) => { PlayerHandler.UpdateStats(_touristManager.BaseStats, _workerManager.BaseStats); };
            cityBuildings.Add(building);
        }
        temp = 0;
        foreach (var c in Config.WorkerBuildings)
        {
            WorkerBuilding building = Instantiate(Config.BuildingBasePrefab, transform).AddComponent<WorkerBuilding>();
            building.transform.position = c.Position;
            building.transform.localRotation = Quaternion.Euler(c.Rotation);
            building.Set(temp, c.Item1, c.Item2);
            temp++;
            building.OnUpgrade += (i) => { PlayerHandler.UpdateStats(_touristManager.BaseStats, _workerManager.BaseStats); };
            workerBuildings.Add(building);
        }

        _touristManager = new(cityBuildings, Config.touristManagerStats, Config.TouristPaths, Config.WorkerVisuals);
        _workerManager = new(ruins, workerBuildings, Config.workerManagerStats, Config.WorkerVisuals);
        PlayerHandler.UpdateStats(_touristManager.BaseStats, _workerManager.BaseStats);
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