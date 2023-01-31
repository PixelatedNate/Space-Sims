using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Planets/planet", order = 1)]
public class Planet : ScriptableObject
{

    [SerializeField]
    public string PlanetName;

    [SerializeField]
    WaittingQuest[] _quests;

    [SerializeField]
    public bool IsStartPlanet;

    public Vector3 PlanetPosition;

    [SerializeField]
    private Sprite _background;
    public Sprite Background { get { return _background; } }

    public Sprite PlanetSprite { get; set; }
    public WaittingQuest[] Quests { get { return _quests; } }


    private Material _orignalMaterial;

  }
