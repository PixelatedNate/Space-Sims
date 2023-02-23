using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Planets/planet", order = 1)]
public class PlanetData : ScriptableObject
{
    [SerializeField]
    public string PlanetName;
    [SerializeField]
    public Sprite Background;
    [SerializeField]
    public Sprite PlanetSprite;
    [SerializeField]
    public bool IsStartPlanet;

    [SerializeField]
    public int QuestRestTimeMinuits;

    [SerializeField]
    public int minNumberOfQuest;
    [SerializeField]
    public int maxNumberOfQuest;

    [SerializeField]
    public QuestData[] quest;
}
