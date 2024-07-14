using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class UpgradableBuildingSO : ScriptableObject
{
    [field :Header("Base info")]

    // UI Data
    [field: SerializeField] public List<Material> Materials { get; private set; }
    [field: SerializeField] public ColliderData Collider { get; private set; }
    [field: SerializeField] public string BName { get; private set; }
    [field: SerializeField] public string Description { get; private set; }

    [field: Space()][field: Header("Upgrade Info")]
    [field: SerializeField] public Resource ResourceToUpgrade {get; private set;}
    [field: SerializeField] public int BasePrice;

    [field: Space()][field: Header("Upgrade Stages")]

    [field: SerializeField] public UpgradeStage[] Stages { get; private set; }
}

[Serializable]
public class UpgradeStage
{
    public Mesh Mesh;
    //public int UpgradeCost; // use formula from config
}
[Serializable]
public struct ColliderData
{
    public Vector3 Size;
    public Vector3 Center;
}