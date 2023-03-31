using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EquipableGearSaveData
{
    public string GearDataName;
    public string PersonId = null;

    public EquipableGearSaveData(EquipableGear gear)
    {
        this.GearDataName = gear.SkillBostingGear.name;
        if (gear.IsEquipedToPerson)
        {
            this.PersonId = SaveSystem.SavedPeople[gear.PersonEquipedTo];
        }
    }

}
