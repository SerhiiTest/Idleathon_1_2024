public class Ruin : BuildingBase
{
    public Path Path { get; private set; }

    public void Set(int id, UpgradableBuildingSO data, Path path)
    {
        Set(id, data);
        Path = path;
    }
}
