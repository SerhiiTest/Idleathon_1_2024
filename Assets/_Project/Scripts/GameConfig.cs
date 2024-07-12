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

    [field: SerializeField] public (UpgradableBuildingSO, Path)[] RuinsBuildings { get; private set; }
    [field: SerializeField] public UpgradableBuildingSO[] CityBuildings { get; private set; }
    
    [field: SerializeField] public Transform BuildingBasePrefab { get; private set; }
}
