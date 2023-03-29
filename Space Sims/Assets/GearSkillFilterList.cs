using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GearSkillFilterList : AbstractSkillFilterList<EquipableGear>
{

    [SerializeField]
    GameObject GearListItemTemplate;
    PersonInfo personInfo;

    public void SetView(PersonInfo personInfo)
    {
        this.personInfo = personInfo;
        base.SetView();
    }

    public override int FilterSkill(EquipableGear a, EquipableGear b, SkillsList skill)
    {
        return a.SkillBostingGear.skills.GetSkill(skill).CompareTo(b.SkillBostingGear.skills.GetSkill(skill));
    }

    public override List<EquipableGear> GetAllItems()
    {
        return GearManager.EquipableGears;
    }

    protected override void SpawnNewUIItemConponent(EquipableGear item, SkillsList? skill)
    {
         GameObject GearViewItem = GameObject.Instantiate(GearListItemTemplate, ScrollPanal);
         GearViewItem.GetComponent<GearListViewItem>().SetGear(item, skill);
         GearViewItem.GetComponent<Button>().onClick.AddListener(() => personInfo.AddGear(item));
    }
}
