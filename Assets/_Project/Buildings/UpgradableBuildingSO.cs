using System;
using UnityEngine;

[CreateAssetMenu()]
public class UpgradableBuildingSO : ScriptableObject
{
    [field: SerializeField] public Material Material { get; private set; }
    [field: SerializeField] public UpgradeStage[] Stages { get; private set; }

    [field: SerializeField] public Resource ResourceToUpgrade {get; private set;} 
}

[Serializable]
public class UpgradeStage
{
    public Mesh Mesh;
    public int UpgradeCost;
}