using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Ruin : Building
{
    private Path _path;

    public void Set(int id, UpgradableBuildingSO data, Path path)
    {
        Set(id, data);
        _path = path;
    }
}
