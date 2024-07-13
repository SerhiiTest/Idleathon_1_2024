public class CityBuilding : BuildingBase
{
    public TouristManagerStats Stats { get; private set; }

    public void Set(int id, UpgradableBuildingSO data, TouristManagerStats stats)
    {
        Set(id, data);
        Stats = stats;
    }
}
