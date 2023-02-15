using UnityEngine;

public class PlanetContainer : ISaveable<PlanetConttainerSaveData>
{

    public PlanetContainer(PlanetData data, Vector3 pos)
    {
        this.planetData = data;
        this.PlanetPosition = pos;
    }

    public PlanetData planetData { get; private set; }
    public Vector3 PlanetPosition;

    public PlanetConttainerSaveData Save()
    {
        PlanetConttainerSaveData savedata = new PlanetConttainerSaveData(this);
        savedata.Save();
        return savedata;
    }

    public void Load(string Path)
    {
        throw new System.NotImplementedException();
    }

    public void Load(PlanetConttainerSaveData data)
    {
        throw new System.NotImplementedException();
    }
}
