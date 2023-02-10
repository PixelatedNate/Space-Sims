using UnityEngine;


public class Planet : ScriptableObject
{

    [SerializeField]
    public string PlanetName;

    [SerializeField]
    WaittingQuest[] _quests;

    [SerializeField]
    public bool IsStartPlanet;

    public Vector3 PlanetPosition;


}
