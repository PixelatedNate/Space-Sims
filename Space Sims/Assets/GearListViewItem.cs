using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GearListViewItem : MonoBehaviour
{
    public EquipableGear Gear { get; private set; }
    [SerializeField]
    TextMeshProUGUI GearName, Modifyer;
    [SerializeField]
    Image icon;

    public void SetGear(EquipableGear Gear, SkillsList? filter)
    {
        this.Gear = Gear;
        icon.sprite = Gear.SkillBostingGear.Image;
        SetStatsAndText(filter);
    }
    private void SetStatsAndText(SkillsList? filter)
    {
        GearName.text = Gear.GearData.Name;

        string ModifyerText = "";
        foreach(SkillsList skillListItem in Enum.GetValues(typeof(SkillsList)))
        {
            if(Gear.SkillBostingGear.skills.GetSkill(skillListItem) > 0)
            {
                ModifyerText += Icons.GetSkillIconForTextMeshPro(skillListItem) + ": " + Gear.SkillBostingGear.skills.GetSkill(skillListItem) + "\n";
            }
        }
        Modifyer.text = ModifyerText;

    }


}
