using Unity.Mathematics;
using UnityEngine;

public class Building : MonoBehaviour
{
    public int Level { get; private set; } = 0;

    private UpgradableBuildingSO data;

    //public int PriceToUpgrade => ;
    
    public void Set(UpgradableBuildingSO data)
    {
        this.data = data;
    }

    public void Upgrade() => Level= math.min(Level + 1, data.Stages.Length);

}
