using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/People/PersonTemplate", order = 1)]
public class PersonTemplate : ScriptableObject
{

    public bool RandomName { get; set; }
    public string PersonName { get; set; }
    public bool RandomGender { get; set; }
    public Gender Gender { get; set; }
    public bool RandomRace { get; set; }
    public Race Race { get; set; }

    public short MinAge { get; set; } = 16;
    public short MaxAge { get; set; } = 100;

    public Skills MinSkills { get; set; } = new Skills();
    public Skills MaxSkills { get; set; } = new Skills();

    public bool RandomHead { get; set; }
    public Sprite Head { get; set; }
    public bool RandomBody { get; set; }
    public Sprite Body { get; set; }

    public bool RandomCloths { get; set; }
    public Sprite Clothes { get; set; }

}
