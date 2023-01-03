using UnityEngine;

public static class SkillColourMap
{


    public static Color GetSkillColour(SkillsList skill)
    {
        if (skill == SkillsList.Strength) { return Color.red; }
        if (skill == SkillsList.Dexterity) { return Color.green; }
        if (skill == SkillsList.Charisma) { return Color.cyan; }
        if (skill == SkillsList.Intelligence) { return Color.blue; }
        if (skill == SkillsList.Wisdom) { return Color.yellow; }

        throw new System.Exception("Skill dose not have a assoiated colour");

    }

}
