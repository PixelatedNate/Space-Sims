using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GearManager
{
    public static List<Gear> gearDatas { get; private set; } = new List<Gear>();
    public static List<EquipableGear> EquipableGears { get; private set; } = new List<EquipableGear>();

}
