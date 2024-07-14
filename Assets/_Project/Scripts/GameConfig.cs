using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class GameConfig : ScriptableObject
{
    [field: SerializeField] public float A { get; private set; } = 0.001f;
    [field: SerializeField] public float B { get; private set; } = 0.02f;
    public int GetNextPrice(int basePrice,int level)
    {
        return (int)Mathf.Round(A * Mathf.Pow(level, 3) * basePrice / 3 + B * Mathf.Pow(level, 2) * basePrice/2 + level * basePrice);
    }

    public TouristManagerStats UpgradeCityStats(TouristManagerStats baseStats, TouristManagerStats stats, int level)
    {
        return baseStats + stats;
    }

    public WorkerManagerStats UpgradeWorkerBuildingsStats(WorkerManagerStats baseStats, WorkerManagerStats stats, int level)
    {
        return baseStats + stats;
    }

    [field: SerializeField] public Transform BuildingBasePrefab { get; private set; }
    [field: SerializeField] public Wallker WallkerPrefab { get; private set; }



    [field: Space()] [field: Header("Worker + Ruins")]
    [field:SerializeField] public WorkerManagerStats workerManagerStats { get; private set; }
    [field: SerializeField] public RuinsBuildingsStruct[] RuinsBuildings { get; private set; }
    [field: SerializeField] public WorkersBuildingsStruct[] WorkerBuildings { get; private set; }
    [field: SerializeField] public float TimeToAutoAction { get; private set; }
    [field: SerializeField] public WallkerVisual[] WorkerVisuals { get; private set; }



    [field: Space()][field: Header("City")]
    [field: SerializeField] public TouristManagerStats touristManagerStats { get; private set; }
    [field: SerializeField] public CityBuildingsStruct[] CityBuildings { get; private set; }
    [field: SerializeField] public Path[] TouristPaths { get; private set; }
    [field: SerializeField] public WallkerVisual[] TouristVisuals { get; private set; }


}








[Serializable]
public struct WallkerVisual
{
    public Material[] Materials;
    public Mesh Mesh;
}

[Serializable]
public struct RuinsBuildingsStruct
{
    public Vector3 Rotation;
    public Vector3 Position;
    public UpgradableBuildingSO Item1;
    public Path Item2;
}

[Serializable]
public struct WorkersBuildingsStruct
{
    public Vector3 Rotation;
    public Vector3 Position;
    public UpgradableBuildingSO Item1;
    public WorkerManagerStats Item2;
}

[Serializable]
public struct CityBuildingsStruct
{
    public Vector3 Rotation;
    public Vector3 Position;
    public UpgradableBuildingSO Item1;
    public TouristManagerStats Item2;
}
