using System;

[Serializable]
public class PlanetConttainerSaveData
{

    public string planetId;
    public string planetDataName;
    public float[] planetPosition = new float[3];

    public PlanetConttainerSaveData(PlanetContainer planetContainer)
    {
        this.planetId = System.Guid.NewGuid().ToString();

        this.planetDataName = planetContainer.planetData.name;
        this.planetPosition[0] = planetContainer.PlanetPosition.x;
        this.planetPosition[1] = planetContainer.PlanetPosition.y;
        this.planetPosition[2] = planetContainer.PlanetPosition.z;

    }


}
