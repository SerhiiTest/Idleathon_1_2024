using System;
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
    [field: SerializeField] public Transform BuildingBasePrefab { get; private set; }



    [field: Space()] [field: Header("Worker + Ruins")]
    [field:SerializeField] public WorkerManagerStats workerManagerStats { get; private set; }
    [field: SerializeField] public RuinsBuildingsStruct[] RuinsBuildings { get; private set; }
    [field: SerializeField] public WorkersBuildingsStruct[] WorkerBuildings { get; private set; }
    [field: SerializeField] public float TimeToAutoAction { get; private set; }



    [field: Space()][field: Header("City")]
    [field: SerializeField] public TouristManagerStats touristManagerStats { get; private set; }
    [field: SerializeField] public CityBuildingsStruct[] CityBuildings { get; private set; }
    [field: SerializeField] public Path[] TouristPaths { get; private set; }
    
   
}

[Serializable]
public struct RuinsBuildingsStruct
{
    public UpgradableBuildingSO Item1;
    public Path Item2;
}

[Serializable]
public struct WorkersBuildingsStruct
{
    public UpgradableBuildingSO Item1;
    public WorkerManagerStats Item2;
}

[Serializable]
public struct CityBuildingsStruct
{
    public UpgradableBuildingSO Item1;
    public TouristManagerStats Item2;
}
