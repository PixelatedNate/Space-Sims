using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractSkillFilterList<T> : MonoBehaviour
{

    [SerializeField]
    protected Transform ScrollPanal;
    protected List<T> Items = null;

    [SerializeField]
    TextMeshProUGUI Filterlable;

    [SerializeField]
    Image FilterArrow;

    [SerializeField]
    Sprite DownFilterArrow, UpFilterArrow;

    [SerializeField]
    bool IncludeAge;

    public static event Action<SkillsList?> OnFiltterChange;

    SkillsList? skillfilter = null;

    private bool Inverted { get; set; }

    protected Action<T> OnSelectMethod = null;
  
    
    private WaittingQuest QuestSelected = null;

    public abstract List<T> GetAllItems();

    public abstract int FilterSkill(T a, T b, SkillsList skill);
    public virtual int FilterByAge(T a, T b) { return int.MaxValue; }

    public void SetView(Predicate<T> filtter = null)
    {
        QuestSelected = null;
        OnSelectMethod = null;
        Items = GetAllItems();
        if (filtter != null)
        {
           Items = Items.FindAll(filtter);
        }
        if (skillfilter == null && IncludeAge)
        {
            FilterListByAge(Inverted);
        }
        else
        {
            skillfilter = SkillsList.Strength;
            FilterListBySkill((SkillsList)skillfilter, Inverted);
        }
        ResetStatsForInheratedStuff();
    }

    protected virtual void ResetStatsForInheratedStuff() // this is becuse quests need to be cleared from people list
    {

    }



    protected void FilterListBySkill(SkillsList skill, bool inverse = false)
    {

        skillfilter = skill;
        Filterlable.text = skill.ToString() + " " + Icons.GetSkillIconForTextMeshPro(skill);

        if (inverse)
        {
            FilterArrow.sprite = UpFilterArrow;
            Items.Sort((a,b) => FilterSkill(b,a,skill));
        }
        else
        {
            FilterArrow.sprite = DownFilterArrow;
            Items.Sort((a,b) => FilterSkill(a,b,skill));
        }
        PopulateList();
        OnFiltterChange?.Invoke(skill);
    }

    private void FilterListByAge(bool inverse = false)
    {
        skillfilter = null;
        Filterlable.text = "Age " + Icons.GetAgeIconForTextMeshPro();
        if (inverse)
        {
            FilterArrow.sprite = UpFilterArrow;
            Items.Sort((a,b) => FilterByAge(b,a));
        }
        else
        {
            FilterArrow.sprite = DownFilterArrow;
            Items.Sort((a,b) => FilterByAge(b,a));
        }

        PopulateList();
        OnFiltterChange?.Invoke(null);
    }


    public void InvertOrder()
    {
        Inverted = !Inverted;
        if (skillfilter == null)
        {
            FilterListByAge(Inverted);
        }
        else
        {
            FilterListBySkill((SkillsList)skillfilter, Inverted);
        }
    }

    public void NextFilter()
    {
        Inverted = false;
        bool isEndOfSkillList = (int)skillfilter == Enum.GetValues(typeof(SkillsList)).Length;
        if (skillfilter == null || (isEndOfSkillList && !IncludeAge))
        {
            FilterListBySkill((SkillsList)1);
            return;
        }
        else if (isEndOfSkillList)
        {
            FilterListByAge();
            return;
        }
        else
        {
            FilterListBySkill((SkillsList)((int)skillfilter + 1));
        }
    }
    public void PreviousFilter()
    {
        Inverted = false;
        bool isStartOfSkillList = (int)skillfilter == 1;
        if (skillfilter == null || (isStartOfSkillList && !IncludeAge))
        {
            FilterListBySkill((SkillsList)Enum.GetValues(typeof(SkillsList)).Length);
            return;
        }
        else if (isStartOfSkillList)
        {
            FilterListByAge();
            return;
        }
        else
        {
            FilterListBySkill((SkillsList)((int)skillfilter - 1));
        }
    }


    private void PopulateList()
    {
        foreach (Transform child in ScrollPanal)
        {
            Destroy(child.gameObject);
        }

        foreach (T item in Items)
        {
            SpawnNewUIItemConponent(item, skillfilter);
        }
    }

   protected abstract void SpawnNewUIItemConponent(T item, SkillsList? skill);

}
