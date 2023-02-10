using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/People/PersonTemplate", order = 1)]
public class PersonTemplate : ScriptableObject
{

    [SerializeField]
    public bool RandomName;
    [SerializeField]
    public string PersonName;
    [SerializeField]
    public bool RandomGender;
    [SerializeField]
    public Gender Gender;
    [SerializeField]
    public bool RandomRace;
    [SerializeField]
    public Race Race;

    [SerializeField]
    public short MinAge;

    [SerializeField]
    public short MaxAge;
    [SerializeField]
    public Skills MinSkills = new Skills();
    
    [SerializeField]
    public Skills MaxSkills  = new Skills();

    [SerializeField]
    public bool RandomHead;
    [SerializeField]
    public Sprite Head;
    [SerializeField]
    public bool RandomBody;
    [SerializeField]
    public Sprite Body;

    [SerializeField]
    public bool RandomCloths;

    [SerializeField]
    public Sprite Clothes;
}
