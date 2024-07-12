using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameConfig GameConfig { get; private set; }
    
    private TouristManager _touristManager;
    private WorkerManager _workerManager;

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
        
        _touristManager = new();
        _workerManager = new();
    }

    public int GetUpgradeCost(int basePrice, int level) => GameConfig.GetNextPrice(basePrice, level);
}
