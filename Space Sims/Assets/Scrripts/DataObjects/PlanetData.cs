using System.Collections;
using System.Collections.Generic;
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
    public QuestData[] quest;
}
