using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipableGear : Gear, ISaveable<EquipableGearSaveData>
{
    public override GearData GearData => SkillBostingGear;
    public SkillBostingGearData SkillBostingGear { get; }

    public PersonInfo PersonEquipedTo { get; set; } = null;
    public bool IsEquipedToPerson { get { return PersonEquipedTo != null; } }

    public EquipableGear(SkillBostingGearData data)
    {
        this.SkillBostingGear = data;
    }

    public EquipableGear(EquipableGearSaveData saveData)
    {
        this.SkillBostingGear = ResourceHelper.GearHelper.GetSkillBostingGear(saveData.GearDataName);
        if(saveData.PersonId != null)
        {
            SaveSystem.LoadedPeople[saveData.PersonId].Gear = this;
        }
    }

    public EquipableGearSaveData Save()
    {
        var gearsaveData = new EquipableGearSaveData(this);
        gearsaveData.Save();
        return gearsaveData;

    }

}
