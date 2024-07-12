using System;
using UnityEngine;

[CreateAssetMenu()]
public class UpgradableBuildingSO : ScriptableObject
{
    [field :Header("Base info")]

    // UI Data
    [field: SerializeField] public Material Material { get; private set; }
    
    [field: Space()][field: Header("Upgrade Info")]
    [field: SerializeField] public Resource ResourceToUpgrade {get; private set;}
    [field: SerializeField] public int BaseCost;

    [field: Space()][field: Header("Upgrade Stages")]

    [field: SerializeField] public UpgradeStage[] Stages { get; private set; }
}

[Serializable]
public class UpgradeStage
{
    public Mesh Mesh;
    //public int UpgradeCost; // use formula from config
}