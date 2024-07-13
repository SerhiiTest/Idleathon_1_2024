using System;
using Unity.Mathematics;
using UnityEngine;

public class BuildingBase : MonoBehaviour
{
    public int Level { get; private set; } = 0;

    protected UpgradableBuildingSO _data;

    public event Action<int> OnUpgrade;

    [field: SerializeField] public int ID { get; private set; }

    public int PriceToUpgrade => GameManager.Instance.GetUpgradeCost(_data.BasePrice, Level + 1);

    public bool IsMaxed { get; internal set; } = false;

    public void Set(int id,UpgradableBuildingSO data)
    {
        ID = id;
        _data = data;

        GetComponent<MeshRenderer>().SetMaterials(data.Materials);
        GetComponent<MeshFilter>().mesh = data.Stages[0].Mesh;
        GetComponent<BoxCollider>().size = data.Collider.Size;
        GetComponent<BoxCollider>().center = data.Collider.Center;
    }


    public void Upgrade() 
    {
        Level = math.min(Level + 1, _data.Stages.Length);
        IsMaxed = Level >= _data.Stages.Length;
        OnUpgrade.Invoke(ID);
    }

}