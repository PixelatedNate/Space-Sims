using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GearManager
{
    public static List<Gear> gearDatas { get; private set; } = new List<Gear>();
    public static List<EquipableGear> EquipableGears { get; private set; } = new List<EquipableGear>();


    public static void LoadGear()
    {
        foreach (EquipableGearSaveData equipableGearSaveData in SaveSystem.GetAllSavedEquipableGear())
        {
            EquipableGears.Add(new EquipableGear(equipableGearSaveData));
        }
    }

    public static void SaveGear()
    {
        foreach(EquipableGear gear in EquipableGears)
        {
            gear.Save();
        }
    }
}
