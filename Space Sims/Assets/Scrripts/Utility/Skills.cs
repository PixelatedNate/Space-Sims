using System;
using UnityEngine;


[Serializable]
public class Skills
{
    private float MaxSkillValue = 10f;

    [SerializeField]
    private float _strength = 2;
    public float Strength { get { return Mathf.Floor(_strength); } set { _strength = Mathf.Clamp(value ,0, MaxSkillValue); } }

    [SerializeField]
    private float _dexterity = 2;
    public float Dexterity { get { return Mathf.Floor(_dexterity); } set { _dexterity = Mathf.Clamp(value ,0, MaxSkillValue); } }
    [SerializeField]
    private float _intelligence = 2;
    public float Intelligence { get { return Mathf.Floor(_intelligence); } set { _intelligence = Mathf.Clamp(value ,0, MaxSkillValue); } }

    [SerializeField]
    private float _wisdom = 2;
    public float Wisdom { get { return Mathf.Floor(_wisdom); } set { _wisdom = Mathf.Clamp(value ,0, MaxSkillValue); } }

    [SerializeField]
    private float _charisma = 2;
    public float Charisma { get { return Mathf.Floor(_charisma); } set { _charisma = Mathf.Clamp(value ,0, MaxSkillValue); } }


    public float GetSkill(SkillsList skill)
    {
        if (skill == SkillsList.Strength)
        {
            return Strength;
        }
        if (skill == SkillsList.Wisdom)
        {
            return Wisdom;
        }
        if (skill == SkillsList.Dexterity)
        {
            return Dexterity;
        }
        if (skill == SkillsList.Intelligence)
        {
            return Intelligence;
        }
        if (skill == SkillsList.Charisma)
        {
            return Charisma;
        }
        else return 0;
    }
    public void SetSkill(SkillsList skill, float value)
    {
        if (skill == SkillsList.Strength)
        {
            _strength = value;
        }
        if (skill == SkillsList.Wisdom)
        {
            _wisdom = value;
        }
        if (skill == SkillsList.Dexterity)
        {
            _dexterity = value;
        }
        if (skill == SkillsList.Intelligence)
        {
            _intelligence = value;
        }
        if (skill == SkillsList.Charisma)
        {
            _charisma = value;
        }
    }

    public void AddToSkill(SkillsList skill, float value)
    {
        if (skill == SkillsList.Strength)
        {
            Strength += value;
        }
        if (skill == SkillsList.Wisdom)
        {
            Wisdom += value;
        }
    }

    #region Comparators
    public static Skills operator +(Skills a, Skills b)
    {
        return new Skills
        { Strength = a.Strength + b.Strength,
            Dexterity = a.Dexterity + b.Dexterity,
            Intelligence = a.Intelligence + b.Intelligence,
            Wisdom = a.Wisdom + b.Wisdom,
            Charisma = a.Charisma + b.Charisma
        };
    }
    public static Skills operator -(Skills a, Skills b)
    {
        return new Skills
        { Strength = a.Strength - b.Strength,
            Dexterity = a.Dexterity - b.Dexterity,
            Intelligence = a.Intelligence - b.Intelligence,
            Wisdom = a.Wisdom - b.Wisdom,
            Charisma = a.Charisma - b.Charisma
        };
    }


    public static bool operator >(Skills a, Skills b)
    {
        return ((a.Strength > b.Strength) &&
                (a.Dexterity > b.Dexterity) &&
                (a.Intelligence > b.Intelligence) &&
                (a.Wisdom > b.Wisdom) &&
                (a.Charisma > b.Charisma));
    }

    public static bool operator <(Skills a, Skills b)
    {
        return ((a.Strength < b.Strength) &&
                (a.Dexterity < b.Dexterity) &&
                (a.Intelligence < b.Intelligence) &&
                (a.Wisdom < b.Wisdom) &&
                (a.Charisma < b.Charisma));
    }

    public static bool operator ==(Skills a, Skills b)
    {
        return ((a.Strength == b.Strength) &&
                (a.Dexterity == b.Dexterity) &&
                (a.Intelligence == b.Intelligence) &&
                (a.Wisdom == b.Wisdom) &&
                (a.Charisma == b.Charisma));
    }

    public static bool operator !=(Skills a, Skills b)
    {
        return ((a.Strength != b.Strength) &&
                (a.Dexterity != b.Dexterity) &&
                (a.Intelligence != b.Intelligence) &&
                (a.Wisdom != b.Wisdom) &&
                (a.Charisma != b.Charisma));
    }

    public static bool operator <=(Skills a, Skills b)
    {
        return ((a.Strength < b.Strength) || (a.Strength == b.Strength) &&
                (a.Dexterity < b.Dexterity) || (a.Dexterity == b.Dexterity) &&
                (a.Intelligence < b.Intelligence) || (a.Intelligence == b.Intelligence) &&
                (a.Wisdom < b.Wisdom) || (a.Wisdom == b.Wisdom) &&
                (a.Charisma < b.Charisma) || (a.Charisma == b.Charisma));
    }
    public static bool operator >=(Skills a, Skills b)
    {
        return ((a.Strength > b.Strength) || (a.Strength == b.Strength) &&
                (a.Dexterity > b.Dexterity) || (a.Dexterity == b.Dexterity) &&
                (a.Intelligence > b.Intelligence) || (a.Intelligence == b.Intelligence) &&
                (a.Wisdom > b.Wisdom) || (a.Wisdom == b.Wisdom) &&
                (a.Charisma > b.Charisma) || (a.Charisma == b.Charisma));
    }

    #endregion

}
