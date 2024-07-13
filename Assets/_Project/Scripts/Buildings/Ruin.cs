using UnityEngine;

public class Ruin : BuildingBase
{
    public Path Path { get; private set; }

    [field: SerializeField] public int Sand { get; private set; }

    public void Set(int id, UpgradableBuildingSO data, Path path)
    {
        Set(id, data);
        Path = path;
    }

    public void AddSand(int amount)
    {
        Sand += amount;
        if(Sand >= PriceToUpgrade)
        {
            Sand -= PriceToUpgrade;
            Upgrade();
        }
    }
}
