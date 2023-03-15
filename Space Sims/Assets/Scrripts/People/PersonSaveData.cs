using System;

[Serializable]
public class PersonSaveData
{

    public string personId;

    public string PersonName;

    public string HomePlanet;
    public string Job;
    public string[] Likes;
    public string[] Dislikes;

    public int Gender;
    public int Race;
    public short Age;

    public float strength;
    public float dexterity;
    public float intelligence;
    public float wisdom;
    public float charisma;


    public string HeadName;
    public string HairName;
    public string BodyName;
    public string ClothsName;


    public float r;
    public float g;
    public float b;

    public PersonSaveData(PersonInfo personInfo)
    {
        this.personId = System.Guid.NewGuid().ToString();

        this.PersonName = personInfo.Name;
        this.HomePlanet = personInfo.HomePlanet;
        this.Job = personInfo.Job;
        this.Likes = personInfo.Likes;
        this.Dislikes = personInfo.Dislikes;

        this.Gender = (int)personInfo.Gender;
        this.Race = (int)personInfo.Race;
        this.Age = personInfo.Age;

        this.strength = personInfo.skills.Strength;
        this.dexterity = personInfo.skills.Dexterity;
        this.intelligence = personInfo.skills.Intelligence;
        this.wisdom = personInfo.skills.Wisdom;
        this.charisma = personInfo.skills.Charisma;

        this.HeadName = personInfo.Head.name;
        this.HairName = personInfo.Hair.name;
        this.BodyName = personInfo.Body.name;
        this.ClothsName = personInfo.Clothes.name;

        this.r = personInfo.SkinColor.r;
        this.g = personInfo.SkinColor.g;
        this.b = personInfo.SkinColor.b;
    }

}
