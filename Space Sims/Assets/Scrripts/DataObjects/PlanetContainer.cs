using UnityEngine;

public class PlanetContainer : ISaveable<PlanetConttainerSaveData>
{

    public PlanetContainer(PlanetData data, Vector3 pos)
    {
        this.planetData = data;
        this.PlanetPosition = pos;
    }

    public PlanetContainer(PlanetConttainerSaveData saveData)
    {
        this.planetData = ResourceHelper.PlanetHelper.GetPlanetData(saveData.planetDataName);
        this.PlanetPosition = new Vector3(saveData.planetPosition[0], saveData.planetPosition[1], saveData.planetPosition[2]);
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
