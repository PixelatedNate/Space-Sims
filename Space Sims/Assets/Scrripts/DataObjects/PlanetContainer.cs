using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetContainer
{

    public PlanetContainer(PlanetData data,  Vector3 pos)
    {
        this.planetData = data;
        this.PlanetPosition = pos;  
    }
  
    public PlanetData planetData { get; private set; }
    public Vector3 PlanetPosition;
}
