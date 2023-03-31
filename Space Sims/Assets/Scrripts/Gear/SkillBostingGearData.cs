using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Gear/SkillBostingGear", order = 1)]
public class SkillBostingGearData : GearData
{
    [SerializeField]
    public Skills skills;
}
