using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipableGear : Gear
{
    public override GearData GearData => SkillBostingGear;
    public SkillBostingGearData SkillBostingGear { get; }

    public PersonInfo PersonEquipedTo { get; set; } = null;
    bool IsEquipedToPerson { get { return PersonEquipedTo != null; } }

    public EquipableGear(SkillBostingGearData data)
    {
        this.SkillBostingGear = data;
    }

}
