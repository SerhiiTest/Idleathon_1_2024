using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Ruin : Building
{
    public Path Path { get; private set; }

    public void Set(int id, UpgradableBuildingSO data, Path path)
    {
        Set(id, data);
        Path = path;
    }
}
